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
		private const string _uUID = "C48C6716-193F-477B-B73A-C550CE582A22";
		private const string _beaconId = "Estimote";

		public Estimote ()
		{
			Logging.Log.WriteLine(String.Format("{0} device created", _beaconId));
		}

		#region IDevice implementation
		public Device GetDevice ()
		{
			return new Device {
				BeaconId = _beaconId,
				UUID = _uUID
			};
		}
		#endregion
	}
}

