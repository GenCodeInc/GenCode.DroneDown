using System;
using System.Collections.Generic;
using System.ComponentModel;
using GenCode.BeaconDevices.Manufacturers;
using GenCode.Logging;

namespace GenCode.DroneDown.ViewModels
{
	/// <summary>
	/// Monitor view model.
	/// </summary>
	class TabPageWelcomeViewModel : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

	    public Device SelectedBeacon { get; set; }

	    private readonly List<Device> _beacons = new List<Device> ();
	    public List<Device>  Beacons {
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

		/// <summary>
		/// Initializes a new instance of the <see cref="GenCode.DroneDown.ViewModels.TabPageWelcomeViewModel"/> class.
		/// 
		/// Set up beacons in the BeaconDevices classes that I want to use by passing in a list of interfaces
		/// 
		/// </summary>
		public TabPageWelcomeViewModel ()
		{
			SetupBeacons (new List<IManufacturer> { new Bkon (), new Estimote (), new Radius () });
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
			} catch (Exception ex) {
				Log.WriteLine (ex);
				throw;
			}
		}
	}
}

