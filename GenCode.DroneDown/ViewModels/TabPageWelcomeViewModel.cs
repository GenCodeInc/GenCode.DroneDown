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
		public GenCode.BeaconDevices.Manufacturers.Device SelectedBeacon {
			get;
			set;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private List<GenCode.BeaconDevices.Manufacturers.Device>  _beacons = new List<GenCode.BeaconDevices.Manufacturers.Device> ();
		public List<GenCode.BeaconDevices.Manufacturers.Device>  Beacons {
			set {
				if (_beacons != value) {
					if (PropertyChanged != null) {
						PropertyChanged (this, 
							new PropertyChangedEventArgs ("Beacons2"));
					}
				}
			}
			get {
				return _beacons;
			}
		}

		public TabPageWelcomeViewModel ()
		{
			SetupBeacons (new List<IManufacturer> { new BKON (), new Estimote (), new Radius () });
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
					Beacons.Add (item.GetDevice);
				}
				//Beacons2.First().BeaconId
			} catch (Exception ex) {
				Logging.Log.WriteLine (ex);
				throw;
			}
		}
	}
}

