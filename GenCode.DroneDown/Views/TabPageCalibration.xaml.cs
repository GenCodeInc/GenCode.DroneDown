using System;
using System.Collections.Generic;
using Xamarin.Forms;
using GenCode.Logging;

namespace GenCode.DroneDown
{	
	public partial class TabPageCalibration : ContentPage
	{	
		public TabPageCalibration ()
		{
			InitializeComponent ();
			Log.WriteLine ("TabPageCalibration, loading.", TraceLogLevel.Verbose);
		}
	}
}

