using System;
using System.ComponentModel;
using Xamarin.Forms;
using GenCode.BeaconDevices.Manufacturers;
using System.Collections.Generic;
using GenCode.Logging;
using System.Linq;

namespace GenCode.DroneDown.ViewModels
{
	/// <summary>
	/// Monitor view model.
	/// </summary>
	class TabPageWelcomeViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private List<string> _beacons = new List<string> ();
		public List<string> Beacons {
			set {
				if (_beacons != value) {
					if (PropertyChanged != null) {
						PropertyChanged (this, 
							new PropertyChangedEventArgs ("Beacons"));
					}
				}
			}
			get {
				return _beacons;
			}
		}

		private List<GenCode.BeaconDevices.Manufacturers.Device>  _beacons2 = new List<GenCode.BeaconDevices.Manufacturers.Device> ();
		public List<GenCode.BeaconDevices.Manufacturers.Device>  Beacons2 {
			set {
				if (_beacons2 != value) {
					if (PropertyChanged != null) {
						PropertyChanged (this, 
							new PropertyChangedEventArgs ("Beacons2"));
					}
				}
			}
			get {
				return _beacons2;
			}
		}

		public TabPageWelcomeViewModel ()
		{
			SetupBeacons (new List<IManufacturer> { new XamarinBeacons (), new BKON (), new Estimote (), new Radius () });

			MessagingCenter.Subscribe<TabPageWelcome> (this, "Hi", (sender) => {
				// do something whenever the "Hi" message is sent
			});
		}

		/// <summary>
		/// Setups the beacons.
		/// </summary>
		/// <param name="manufs">Manufs.</param>
		private void SetupBeacons (List<IManufacturer> manufs)
		{
			try { 
				Log.WriteLine ("Setting up beacons, loading.", TraceLogLevel.Verbose);

				foreach (var item in manufs) {
					Beacons.Add (item.GetDevice.BeaconId);
				}

				foreach (var item in manufs) {
					Beacons2.Add (item.GetDevice);
				}
				//Beacons2.First().BeaconId
			} catch (Exception ex) {
				Logging.Log.WriteLine (ex);
				throw;
			}
		}
	}
}

