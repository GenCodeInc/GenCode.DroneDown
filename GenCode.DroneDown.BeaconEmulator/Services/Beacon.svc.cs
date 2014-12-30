using GenCode.BeaconDevices.Manufacturers;
using GenCode.DroneDown.BeaconEmulator.Interfaces;

namespace GenCode.DroneDown.BeaconEmulator.Services
{
    public class Beacon : IBeacon
    {
        public bool CheckVersion(string version)
        {
            throw new System.NotImplementedException();
        }

     
        System.Collections.Generic.List<BeaconDevices.BeaconDataPackage> IBeacon.Emulate(XamarinBeaconsLLC beacon)
        {
            throw new System.NotImplementedException();
        }
    }
}
