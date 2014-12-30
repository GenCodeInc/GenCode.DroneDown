using System.Runtime.Serialization;

namespace GenCode.BeaconDevices.Manufacturers
{
	/// <summary>
	/// Manufacturer.
	/// 
	/// Manufacturer of a iBeacon device
	/// </summary>
    [DataContract]
    public class BeaconDevice
    {
        [DataMember]
        public string Uuid { get; set; }

        [DataMember]
        public string BeaconId { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}

