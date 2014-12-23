using System;
using System.Collections.Generic;
using System.Linq;

namespace GenCode.DroneDown.Classes
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

		public static bool Calibrated;
		public static Coefficient CoefficientsToUse = Coefficients.First();

		public static int TxPowerRssi {
			get;
			set;
		}


		public static double CalculateDistance(double rssi, Coefficient coefficient = null) {

			if (Math.Abs(rssi) <= 0) {
				return -1.0; // if we cannot determine accuracy, return -1.
			}

			// set default
			if (TxPowerRssi == 0)
				TxPowerRssi = -56;

			if (coefficient == null)
				coefficient = CoefficientsToUse;

			double ratio = rssi*1.0/TxPowerRssi;
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
		    TxPowerRssi = rssiList.Sum() / rssiList.Count;

			double prevDistance = 1000;
			CoefficientsToUse = Coefficients.First();

			// now see what Coefficient comes cloest to 1 meter
			foreach (var item in Coefficients) {
				var distance = CalculateDistance (TxPowerRssi, item);
				if (distance - 1.0 < prevDistance - 1.0) {
					prevDistance = distance;
					CoefficientsToUse = item;
				}
			}

			Calibrated = true;
		}
	}
}

