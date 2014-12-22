using System;
using System.Collections.Generic;
using Xamarin.Forms;
using GenCode.Logging;
using System.Linq;
using GenCode.BeaconDevices.Manufacturers;
using GenCode.DroneDown.ViewModels;

namespace GenCode.DroneDown
{	
	/// <summary>
	/// Welcome page that lets the use select their beacon
	/// </summary>
	public partial class TabPageWelcome : ContentPage
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

			GenCode.BeaconDevices.Default.Device = (GenCode.BeaconDevices.Manufacturers.Device)e.Item;

			((ListView)sender).SelectedItem = null; // de-select the row
		}
	}
}

