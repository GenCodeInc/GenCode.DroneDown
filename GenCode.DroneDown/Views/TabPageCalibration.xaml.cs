using GenCode.Logging;
using Xamarin.Forms;

namespace GenCode.DroneDown.Views
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

