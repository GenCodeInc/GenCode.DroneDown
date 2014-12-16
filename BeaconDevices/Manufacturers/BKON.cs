using System;

namespace GenCode.BeaconDevices.Manufacturers
{
	public class BKON : IManufacturer
	{
		private const string _uUID = "C48C6716-193F-477B-B73A-C550CE582A22";
		private const string _beaconId = "BKON";

		public BKON ()
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

