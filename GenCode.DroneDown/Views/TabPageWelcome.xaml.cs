using System;
using System.Collections.Generic;
using Xamarin.Forms;
using GenCode.Logging;

namespace GenCode.DroneDown
{	
	public partial class TabPageWelcome : ContentPage
	{	
		public TabPageWelcome ()
		{
			InitializeComponent ();

			Log.WriteLine ("TabPageWelcome, loading.", TraceLogLevel.Verbose);
		}
	}
}

