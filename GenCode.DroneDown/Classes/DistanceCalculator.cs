using System;
using System.Diagnostics;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace GenCode.DroneDown
{
	public class Coefficient
	{
		public double Coefficient1;
		public double Coefficient2;
		public double Coefficient3;
	}

	public static class DistanceCalculator
	{
		public static List<Coefficient> Coefficients = new List<Coefficient> { 
//			new Coefficient { Coefficient1 = 100.89976,  Coefficient2 = 100.7095, Coefficient3 = 100.111 },
			new Coefficient { Coefficient1 = 0.89976,  Coefficient2 = 7.7095, Coefficient3 = 0.111 },
			new Coefficient { Coefficient1 = 0.42093,  Coefficient2 = 6.9476, Coefficient3 = 0.54992 } 
		};

		public static bool Calibrated = false;
		public static Coefficient CoefficientsToUse = Coefficients.First();

		public static int TxPowerRSSI {
			get;
			set;
		}


		public static double CalculateDistance(double rssi, Coefficient coefficient = null) {

			if (rssi == 0) {
				return -1.0; // if we cannot determine accuracy, return -1.
			}

			// set default
			if (TxPowerRSSI == 0)
				TxPowerRSSI = -56;

			if (coefficient == null)
				coefficient = CoefficientsToUse;

			double ratio = rssi*1.0/TxPowerRSSI;
			double distance = ratio < 1.0 ? Math.Pow (ratio, 10) :
				(coefficient.Coefficient1) * Math.Pow (ratio, coefficient.Coefficient2) + coefficient.Coefficient3;

			return distance;
		}

		/// <summary>
		/// Pass in a list of RSSI's to use to calibrate
		/// </summary>
		/// <param name="rssiList">Rssi list.</param>
		public static void Calibrate(List<int> rssiList)
		{
			// first get the average RSSI
			int total = 0;
			foreach (var item in rssiList) {
				total += item;
			}
			TxPowerRSSI = total / rssiList.Count;

			double prevDistance = 1000;
			CoefficientsToUse = Coefficients.First();

			// now see what Coefficient comes cloest to 1 meter
			foreach (var item in Coefficients) {
				var distance = CalculateDistance (TxPowerRSSI, item);
				if (distance - 1.0 < prevDistance - 1.0) {
					prevDistance = distance;
					CoefficientsToUse = item;
				}
			}

			Calibrated = true;
		}
	}
}

