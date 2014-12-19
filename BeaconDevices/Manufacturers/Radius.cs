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
		private const string _uUID = "2F234454-cf6d4a0f-adf2-f4911ba9ffa6";
		private const string _beaconId = "Radius";

		#region IManufacturer implementation

		public Device GetDevice {
			get { return new Device {
					BeaconId = _beaconId,
					UUID = _uUID
				};
			}
		}

		#endregion

		public Radius ()
		{
			Logging.Log.WriteLine(String.Format("{0} device created", _beaconId));
		}
	}
}

