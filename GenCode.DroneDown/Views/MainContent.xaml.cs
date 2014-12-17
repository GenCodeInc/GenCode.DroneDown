using System;
using System.Collections.Generic;
using Xamarin.Forms;
using GenCode.Logging;

namespace GenCode.DroneDown
{	
	public partial class MainContent : TabbedPage
	{	
		public readonly TabPageWelcome WelcomePage = new TabPageWelcome ();
		public readonly TabPageMonitor MonitorPage = new TabPageMonitor ();
		public readonly TabPageCalibration CalibrationPage = new TabPageCalibration ();

		public MainContent ()
		{
			InitializeComponent ();
			Log.WriteLine ("MainContent, Loading tabs.", TraceLogLevel.Verbose);

			this.Children.Add (WelcomePage);
			this.Children.Add (MonitorPage);
			this.Children.Add (CalibrationPage);
		}
	}
}

