using System;

namespace GenCode.BeaconDevices.Manufacturers
{
	/// <summary>
	/// BKON.
	/// 
	/// Creates a bkon manufacturer
	/// http://www.bkon.com/
	/// </summary>
	public class Bkon : IManufacturer
	{
		public Device GetDevice {
			get {
				return new Device {
					BeaconId = "BKON",
					Uuid = "C48C6716-193F-477B-B73A-C550CE582A22",
					Description = "Look for a BKON beacon http://www.bkon.com"
				};			
			}
		}

		public Bkon ()
		{
			Logging.Log.WriteLine (String.Format ("{0} device created", GetDevice.BeaconId));
		}
	}
}

