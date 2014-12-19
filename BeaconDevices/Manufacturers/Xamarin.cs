using System;

namespace GenCode.BeaconDevices.Manufacturers
{
	/// <summary>
	/// Xamarin.
	/// 
	/// Creates a Xamarin manufacturer
	/// 
	/// Wait a minute, Xamarin does not make beacons, well then we are going to get fake data from another datasource
	/// Probably a WCF service if I have time
	///  
	/// http://www.xamarin.com/
	/// </summary>
	public class XamarinBeacons : IManufacturer
	{
		private const string _uUID = "00000000-0000-0000-0000-000000000000";
		private const string _beaconId = "Xamarin";

		#region IManufacturer implementation

		public Device GetDevice {
			get { return new Device {
					BeaconId = _beaconId,
					UUID = _uUID
				};
			}
		}

		#endregion

		public XamarinBeacons ()
		{
			Logging.Log.WriteLine(String.Format("{0} device created", _beaconId));
		}
	}
}

