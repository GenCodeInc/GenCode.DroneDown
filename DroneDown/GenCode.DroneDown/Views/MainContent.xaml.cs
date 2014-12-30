using GenCode.DroneDown.ServiceReference1;
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

            Log.WriteLine("MainContent, Loaded tabs.", TraceLogLevel.Verbose);
        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
            var bc = new BeaconClient();

            bc.CheckLatestVersionCompleted += bc_CheckLatestVersionCompleted;
            bc.CheckLatestVersionAsync("123");
        }

	    void bc_CheckLatestVersionCompleted(object sender, CheckLatestVersionCompletedEventArgs e)
	    {
	        var result = e.Result;
            Device.BeginInvokeOnMainThread(() => DisplayAlert("version checked", "message", "CancelEventArgs"));
        }
	}
}

