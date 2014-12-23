namespace GenCode.BeaconDevices.Manufacturers
{
	/// <summary>
	/// Manufacturer.
	/// 
	/// Manufacturer of a iBeacon device
	/// </summary>
	public class Device
	{
	    private string _uuid;
	    private string _beaconId;
	    private string _description;

	    public string Uuid
	    {
	        get { return _uuid; }
	        set { _uuid = value; }
	    }

	    public string BeaconId
	    {
	        get { return _beaconId; }
	        set { _beaconId = value; }
	    }

	    public string Description
	    {
	        get { return _description; }
	        set { _description = value; }
	    }
	}
}

