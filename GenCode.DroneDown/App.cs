using System;
using GenCode.DroneDown.Views;
using Xamarin.Forms;
using GenCode.Logging;

namespace GenCode.DroneDown
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			Log.WriteLine ("GetMainPage, Drone Down application has started.", TraceLogLevel.Verbose);

			return new MainContent ();
		}
	}
}

