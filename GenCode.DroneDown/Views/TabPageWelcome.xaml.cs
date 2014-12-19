using System;
using System.Collections.Generic;
using Xamarin.Forms;
using GenCode.Logging;
using System.Linq;
using GenCode.BeaconDevices.Manufacturers;

namespace GenCode.DroneDown
{	
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
		void OnItemSelected (object sender, ItemTappedEventArgs e) {

			if (e == null) return; // has been set to null, do not 'process' tapped event

			MessagingCenter.Send<TabPageWelcome, string> (this, "Hi", "John");

			Log.WriteLine ("Tapped: " + e.Item, TraceLogLevel.Verbose);
			((ListView)sender).SelectedItem = null; // de-select the row
		}
	}
}

