using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using RadiusNetworks.IBeaconAndroid;
using Xamarin.Forms;
using System.Linq;
using GenCode.BeaconDevices;
using GenCode.DroneDown.Android.Classes;
using GenCode.DroneDown.Views;
using GenCode.Logging;
using Device = GenCode.BeaconDevices.Manufacturers.Device;
using MonitorNotifier = GenCode.DroneDown.Android.Classes.MonitorNotifier;


namespace GenCode.DroneDown.Android
{
    [Activity(Label = "GenCode.DroneDown.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AndroidActivity, IBeaconConsumer
    {
        IBeaconManager _iBeaconManager;
        MonitorNotifier _monitorNotifier;
        RangeNotifier _rangeNotifier;
        Region _monitoringRegion;
        Region _rangingRegion;

        public MainActivity()
        {
            try
            {
                MessagingCenter.Subscribe<TabPageWelcome, Device>(this, "DeviceChanged", (sender, messageData) => {
                        SetupBeacons(messageData);
                        Log.WriteLine("DeviceChanged, look for new beacon");
                    });
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex);
                throw;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);
                Forms.Init(this, bundle);

                // This redundent variable is required to initialize the infragistics controls per documentation in the beta
                // supressing warning
#pragma warning disable 219
                // ReSharper disable once UnusedVariable
                var type = typeof(Infragistics.Xamarin.Android.RadialGaugeViewRenderer);
#pragma warning restore 219

                // setting up a beacon by passing in any device you want
                BeaconDevices.Default.GetDevice = new BeaconDevices.Manufacturers.Bkon().GetDevice;
                SetupBeacons(BeaconDevices.Default.GetDevice);

                // Setup the page
                SetPage(App.GetMainPage());
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Setups your beacon.
        /// </summary>
        /// <param name="device">manufucturer.</param>
        private void SetupBeacons(Device device)
        {
            try
            {
                if (device != null)
                {
                    ShutdownBeacon();

                    _iBeaconManager = IBeaconManager.GetInstanceForApplication(this);
                    _monitorNotifier = new MonitorNotifier();
                    _rangeNotifier = new RangeNotifier();

                    _monitoringRegion = new Region(device.BeaconId, device.Uuid, null, null);
                    _rangingRegion = new Region(device.BeaconId, device.Uuid, null, null);

                    if (_iBeaconManager != null)
                    {
                        _iBeaconManager.Bind(this);
                        _monitorNotifier.EnterRegionComplete += EnteredRegion;
                        _monitorNotifier.ExitRegionComplete += ExitedRegion;

                        _rangeNotifier.DidRangeBeaconsInRegionComplete += RangingBeaconsInRegion;
                        // _iBeaconManager.StartMonitoringBeaconsInRegion(_rangingRegion);
                        //_iBeaconManager.StartRangingBeaconsInRegion(_rangingRegion);
                    }
                    else
                    {
                        Log.WriteLine("iBeaconManager is null and has not been set", TraceLogLevel.Info);
                    }


                    Log.WriteLine("Setup beacon method complete", TraceLogLevel.Verbose);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// This shutsdown a beacon if its running, it also is used by dispose to 
        /// cleans up any running beacons and the notifiers
        /// </summary>
        private void ShutdownBeacon()
        {
            if (_iBeaconManager != null)
            {
                _iBeaconManager.StopRangingBeaconsInRegion(_rangingRegion);
                _iBeaconManager.StopMonitoringBeaconsInRegion(_rangingRegion);
                _iBeaconManager.SetRangeNotifier(null);
                _iBeaconManager.SetMonitorNotifier(null);
            }
            if (_monitoringRegion != null)
            {
                _monitoringRegion.Dispose();
                _monitoringRegion = null;
            }
            if (_monitorNotifier != null)
            {
                _monitorNotifier.Dispose();
                _monitorNotifier = null;
            }
            if (_rangingRegion != null)
            {
                _rangingRegion.Dispose();
                _rangingRegion = null;
            }
            if (_rangeNotifier != null)
            {
                _rangeNotifier.Dispose();
                _rangeNotifier = null;
            }
            if (_iBeaconManager != null)
            {
                _iBeaconManager.UnBind(this);
                _iBeaconManager.Dispose();
            }
            _iBeaconManager = null;
        }


        /// <summary>
        /// Raises the I beacon service connect event.
        /// </summary>
        public void OnIBeaconServiceConnect()
        {
            try
            {
                _iBeaconManager.SetMonitorNotifier(_monitorNotifier);
                _iBeaconManager.SetRangeNotifier(_rangeNotifier);

                _iBeaconManager.StartMonitoringBeaconsInRegion(_monitoringRegion);
                _iBeaconManager.StartRangingBeaconsInRegion(_rangingRegion);

                Log.WriteLine("Beacon service connected", TraceLogLevel.Verbose);
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Entereds the region callback.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void EnteredRegion(object sender, MonitorEventArgs e)
        {
            Log.WriteLine("Entered Region", TraceLogLevel.Verbose);
        }

        /// <summary>
        /// Exiteds the region callback.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void ExitedRegion(object sender, MonitorEventArgs e)
        {
            Log.WriteLine("Exited Region", TraceLogLevel.Verbose);
        }

        /// <summary>
        /// This is the Radius networks callback when ranging a beacon
        /// Here we will call Messaging Center to anyone who is listining
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void RangingBeaconsInRegion(object sender, RangeEventArgs e)
        {
            try
            {
                var found = false;
                var rssi = 0;
                var accuracy = -999D;

                if (e.Beacons.Count > 0)
                {
                    found = true;
                    var beacon = e.Beacons.FirstOrDefault();
                    if (beacon != null)
                    {
                        rssi = beacon.Rssi;
                        accuracy = beacon.Accuracy;
                    }
                }

                // Send the message
                MessagingCenter.Send(((MainContent)App.GetMainPage()).MonitorPage, "BeaconMessage", new BeaconDataPackage {Found = found, Rssi =  rssi, Distance = accuracy});

                // log whats happening
                Log.WriteLine(String.Format("RangingBeaconsInRegion for {0} sending message rssi {1} accuracy {2}", BeaconDevices.Default.GetDevice.BeaconId, rssi, accuracy), TraceLogLevel.Verbose);
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex);
                throw;
            }
        }

        void IDisposable.Dispose()
        {
            ShutdownBeacon();

            base.Dispose();
        }
    }
}


