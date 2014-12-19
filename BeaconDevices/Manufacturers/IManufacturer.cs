namespace GenCode.BeaconDevices.Manufacturers
{
	/// <summary>
	/// I manufacturer.
	/// 
	/// Interface to create a manufacturer of a device
	/// </summary>
	public interface IManufacturer
	{
		Device GetDevice {
			get;
		}
	}
}

