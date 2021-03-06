﻿using System;
using System.Collections.Generic;
using GenCode.BeaconDevices.Manufacturers;
using GenCode.Logging;

namespace GenCode.DroneDown.ViewModels
{
    /// <summary>
    /// Monitor view model.
    /// </summary>
    class TabPageWelcomeViewModel : BaseViewModel
    {
        private List<BeaconDevice> _beacons = new List<BeaconDevice>();
        public List<BeaconDevice> Beacons
        {
            set
            {
                _beacons = value;
                OnPropertyChanged();
            }
            get
            {
                return _beacons;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenCode.DroneDown.ViewModels.TabPageWelcomeViewModel"/> class.
        /// 
        /// Set up beacons in the BeaconDevices classes that I want to use by passing in a list of interfaces
        /// 
        /// </summary>
        public TabPageWelcomeViewModel()
        {
            SetupBeacons(new List<IManufacturer> { new XamarinBeacons(), new Bkon(), new Estimote(), new Radius() });
        }

        /// <summary>
        /// Setups the beacons.
        /// </summary>
        /// <param name="manufs">Manufs.</param>
        private void SetupBeacons(List<IManufacturer> manufs)
        {
            try
            {
                Log.WriteLine("Setting up beacons, loading.", TraceLogLevel.Verbose);

                foreach (var item in manufs)
                {
                    Beacons.Add(item.GetBeaconDevice);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex);
                throw;
            }
        }
    }
}

