using GenCode.Logging;
using Xamarin.Forms;
using Device = GenCode.BeaconDevices.Manufacturers.Device;

namespace GenCode.DroneDown.Views
{	
	/// <summary>
	/// Welcome page that lets the use select their beacon
	/// </summary>
	public partial class TabPageWelcome
	{	

		public TabPageWelcome () {
			InitializeComponent ();

			Log.WriteLine ("TabPageWelcome, loading.", TraceLogLevel.Verbose);

		}

		/// <summary>
		/// Raises the item tapped event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnItemTapped (object sender, ItemTappedEventArgs e) {
			if (e == null) return; // has been set to null, do not 'process' tapped event

			Log.WriteLine ("Tapped: " + e.Item, TraceLogLevel.Verbose);

            // Change the
            BeaconDevices.Default.GetDevice = (Device)e.Item;

            // Broadcast that the user selected another beacon
            MessagingCenter.Send(((MainContent)App.GetMainPage()).WelcomePage, "DeviceChanged", BeaconDevices.Default.GetDevice);


			((ListView)sender).SelectedItem = null; // de-select the row
		}
	}
}

