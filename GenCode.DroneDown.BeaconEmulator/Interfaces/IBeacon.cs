using System.Collections.Generic;
using System.ServiceModel;

namespace GenCode.DroneDown.BeaconEmulator.Interfaces
{
    [ServiceContract]
    public interface IBeacon
    {
        [OperationContract]
        bool CheckVersion(string version);

        [OperationContract]
        List<BeaconDevices.BeaconDataPackage> Emulate(BeaconDevices.Manufacturers.XamarinBeaconsLLC beacon);
    }
}
