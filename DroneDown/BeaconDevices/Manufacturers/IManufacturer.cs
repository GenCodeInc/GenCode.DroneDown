namespace GenCode.BeaconDevices.Manufacturers
{
	/// <summary>
	/// I manufacturer.
	/// 
	/// Interface to create a manufacturer of a BeaconDevice
	/// </summary>
	public interface IManufacturer
	{
		BeaconDevice GetBeaconDevice {
			get;
		}
	}
}

