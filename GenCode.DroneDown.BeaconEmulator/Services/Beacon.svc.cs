using System.Collections.Generic;
using GenCode.BeaconDevices;
using GenCode.BeaconDevices.Manufacturers;
using GenCode.DroneDown.BeaconEmulator.Interfaces;

namespace GenCode.DroneDown.BeaconEmulator.Services
{
    public class Beacon : IBeacon
    {
        public bool CheckLatestVersion(string version)
        {
            return true;
        }


        public List<BeaconData> Emulate(IManufacturer manufacturer)
        {
            return null;// Emulator.Get(beacon);
        }
    }
}
