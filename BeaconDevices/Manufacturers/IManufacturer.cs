namespace GenCode.BeaconDevices.Manufacturers
{
	/// <summary>
	/// I manufacturer.
	/// 
	/// Creates a manufacturer and lets you assign manuf default device data to it, like UUID etc
	/// </summary>
	public interface IManufacturer
	{
		Manufacturer GetDevice();
	}
}

