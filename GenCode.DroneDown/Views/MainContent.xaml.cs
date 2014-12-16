using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GenCode.DroneDown
{	
	public partial class MainContent : TabbedPage
	{	
		private readonly TabPageWelcome WelcomePage = new TabPageWelcome ();
		private readonly TabPageMonitor MonitorPage = new TabPageMonitor ();
		private readonly TabPageCalibration CalibrationPage = new TabPageCalibration ();

		public MainContent ()
		{
			InitializeComponent ();

			this.Children.Add (WelcomePage);
			this.Children.Add (MonitorPage);
			this.Children.Add (CalibrationPage);
		}
	}
}

