using System;
using Android.App;
using Android.Content.PM;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using RadiusNetworks.IBeaconAndroid;
using Xamarin.Forms;
using System.Linq;
using GenCode.Logging;


namespace GenCode.DroneDown.Android
{
	[Activity (Label = "GenCode.DroneDown.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : AndroidActivity, IBeaconConsumer
	{
		/// <summary>
		/// Xamarin Team: 
		/// I use _ for my private variables, some workplaces use upper for both properties and variables, im open to change if needed
		/// </summary>
		IBeaconManager _iBeaconManager;
		MonitorNotifier _monitorNotifier;
		RangeNotifier _rangeNotifier;
		Region _monitoringRegion;
		Region _rangingRegion;

		public MainActivity ()
		{
			try
			{
				// setting up a beacon to test by passing in any manuf you want, this case I want a BKON device.
				SetupBeacons (new GenCode.BeaconDevices.Manufacturers.BKON ());
			}
			catch(Exception ex) {
				Logging.Log.WriteLine(ex);
				throw;
			}
		}

		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);

				// This is required to initialize the infragistics controls per documentation, supressing warning
				#pragma warning disable 219
				var type = typeof(Infragistics.Xamarin.Android.RadialGaugeViewRenderer);
				#pragma warning restore 219

				Xamarin.Forms.Forms.Init (this, bundle);

				_iBeaconManager.Bind (this);
				_monitorNotifier.EnterRegionComplete += EnteredRegion;
				_monitorNotifier.ExitRegionComplete += ExitedRegion;

				_rangeNotifier.DidRangeBeaconsInRegionComplete += RangingBeaconsInRegion;


				SetPage (App.GetMainPage ());
			}
			catch(Exception ex) {
				Logging.Log.WriteLine(ex);
				throw;
			}
		}

		/// <summary>
		/// Setups the beacons.
		/// 
		/// Xamarin Team: 
		/// This is a method to create a device, it takes an interface(IManufacturer) you can pass it any manuf you like.
		/// Although I did not need to do it this way, I wanted to show Xamarin the useage of Interfaces and Dependency Injection
		/// </summary>
		/// <param name="manuf">manufucturer.</param>
		private void SetupBeacons(GenCode.BeaconDevices.Manufacturers.IManufacturer manuf)
		{
			try
			{
				var device = manuf.GetDevice ();

				_iBeaconManager = IBeaconManager.GetInstanceForApplication (this);
				_monitorNotifier = new MonitorNotifier ();
				_rangeNotifier = new RangeNotifier ();

				_monitoringRegion = new Region (device.BeaconId, device.UUID, null, null);
				_rangingRegion = new Region (device.BeaconId, device.UUID, null, null);
			}
			catch(Exception ex) {
				Logging.Log.WriteLine (ex);
				throw;
			}
		}


		/// <summary>
		/// Raises the I beacon service connect event.
		/// </summary>
		public void OnIBeaconServiceConnect ()
		{
			try
			{
				_iBeaconManager.SetMonitorNotifier (_monitorNotifier);
				_iBeaconManager.SetRangeNotifier (_rangeNotifier);

				_iBeaconManager.StartMonitoringBeaconsInRegion (_monitoringRegion);
				_iBeaconManager.StartRangingBeaconsInRegion (_rangingRegion);
			}
			catch(Exception ex) {
				Logging.Log.WriteLine (ex);
				throw;
			}
		}

		/// <summary>
		/// Entereds the region callback.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void EnteredRegion (object sender, MonitorEventArgs e)
		{
			Log.WriteLine ("Entered Region", TraceLogLevel.Verbose);
		}

		/// <summary>
		/// Exiteds the region callback.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void ExitedRegion (object sender, MonitorEventArgs e)
		{
			Log.WriteLine ("Exited Region", TraceLogLevel.Verbose);
		}

		/// <summary>
		/// This is the Radius networks callback when ranging a beacon
		/// Here we will call Messaging Center to anyone who is listining
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void RangingBeaconsInRegion (object sender, RangeEventArgs e)
		{
			bool found = false;
			int rssi = 0;
			double accuracy = -999;

			if (e.Beacons.Count > 0) {
				found = true;
				var beacon = e.Beacons.FirstOrDefault ();
				rssi = beacon.Rssi;
				accuracy = beacon.Accuracy;
			}

			// Send the message, lets use a Tuple to prevent having to make a new class. 
			MessagingCenter.Send (((MainContent)App.GetMainPage ()).MonitorPage, "BeaconMessage", new Tuple<bool, int, double> (found, rssi, accuracy));
			Log.WriteLine (String.Format ("RangingBeaconsInRegion sending message rssi {0} accuracy {1}", rssi, accuracy), TraceLogLevel.Verbose);
		}
	}
}


