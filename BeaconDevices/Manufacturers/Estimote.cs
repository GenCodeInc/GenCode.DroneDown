using System;

namespace GenCode.BeaconDevices.Manufacturers
{
	/// <summary>
	/// Estimote.
	/// 
	/// Creates Estimote manufacturer
	/// http://estimote.com/
	/// 
	/// </summary>
	public class Estimote : IManufacturer
	{
		#region IManufacturer implementation

		public Device GetDevice {
			get {
				return new Device {
					BeaconId = "Estimote",
					Uuid = "C48C6716-193F-477B-B73A-C550CE582A22",
					Description = "Look for a Estimote beacon http://estimote.com"
				};
			}
		}

		#endregion

		public Estimote ()
		{
			Logging.Log.WriteLine (String.Format ("{0} device created", GetDevice.BeaconId));
		}
	}
}

