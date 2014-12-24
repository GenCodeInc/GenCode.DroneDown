using System;

namespace GenCode.BeaconDevices.Manufacturers
{
	/// <summary>
	/// Radius.
	/// 
	/// This creates a Radius manufactur
	/// http://www.radiusnetworks.com/
	/// 
	/// </summary>
	public class Radius : IManufacturer
	{
		#region IManufacturer implementation

		public Device GetDevice {
			get {
				return new Device {
					BeaconId = "Radius",
					Uuid = "2F234454-cf6d4a0f-adf2-f4911ba9ffa6",
					Description = "Look for a BKON beacon http://www.radiusnetworks.com"
				};
			}
		}

		#endregion

		public Radius ()
		{
			Logging.Log.WriteLine (String.Format ("{0} device created", GetDevice.BeaconId));
		}
	}
}

