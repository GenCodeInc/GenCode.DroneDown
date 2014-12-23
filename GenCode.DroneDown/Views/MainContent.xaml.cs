using GenCode.Logging;
using Xamarin.Forms;

namespace GenCode.DroneDown.Views
{	
	public partial class MainContent
	{	
		public readonly TabPageWelcome WelcomePage = new TabPageWelcome ();
		public readonly TabPageMonitor MonitorPage = new TabPageMonitor ();
		public readonly TabPageCalibration CalibrationPage = new TabPageCalibration ();

		public MainContent ()
		{
			InitializeComponent ();
			Log.WriteLine ("MainContent, Loading tabs.", TraceLogLevel.Verbose);

			Children.Add (WelcomePage);
			Children.Add (MonitorPage);
			Children.Add (CalibrationPage);
		}
	}
}

