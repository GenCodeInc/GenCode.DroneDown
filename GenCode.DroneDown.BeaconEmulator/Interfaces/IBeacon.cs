using GenCode.DroneDown.BeaconEmulator.Services;
using System.Collections.Generic;
using System.ServiceModel;
using GenCode.BeaconDevices;
using GenCode.BeaconDevices.Manufacturers;

namespace GenCode.DroneDown.BeaconEmulator.Interfaces
{
    [ServiceContract]
    public interface IBeacon
    {
        [OperationContract]
        bool CheckLatestVersion(string version);

        [OperationContract]
        List<BeaconData> Emulate(IManufacturer manufacturer);
    }
}
