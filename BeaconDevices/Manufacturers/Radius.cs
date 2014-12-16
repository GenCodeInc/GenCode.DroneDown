using System;

namespace GenCode.BeaconDevices.Manufacturers
{
	public class Radius : IManufacturer
	{
		private const string _uUID = "2F234454-cf6d4a0f-adf2-f4911ba9ffa6";
		private const string _beaconId = "Radius";

		public Radius ()
		{
			System.Diagnostics.Debug.WriteLine(String.Format("{0} device created", _beaconId));
		}

		#region IDevice implementation
		public Manufacturer GetDevice ()
		{
			return new Manufacturer {
				BeaconId = _beaconId,
				UUID = _uUID
			};
		}
		#endregion
	}
}

