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
		#region IManufacturer implementation

		public Device GetDevice {
			get {
				return new Device {
					BeaconId = "Xamarin",
					Uuid = "00000000-0000-0000-0000-000000000000",
					Description = "Simulates beacon data, uses Amazon Web Services"
				};
			}
		}

		#endregion

		public XamarinBeacons ()
		{
			Logging.Log.WriteLine (String.Format ("{0} device created", GetDevice.BeaconId));
		}
	}
}

