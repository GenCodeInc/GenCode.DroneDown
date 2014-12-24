using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GenCode.DroneDown.Classes;
using GenCode.DroneDown.Views;
using Xamarin.Forms;

namespace GenCode.DroneDown.ViewModels
{
	/// <summary>
	/// Monitor view model.
	/// </summary>
    class TabPageMonitorViewModel : BaseViewModel
	{
        string _message = "";
        public string Message
        {
            get { return _message; }
            set
            {
                if (value.Equals(_message, StringComparison.Ordinal)) return;

                _message = value;
                OnPropertyChanged();
            }
        }

        int _rssi;
        public int Rssi
        {
            get { return _rssi; }
            set
            {
                if (value == _rssi) return;

                _rssi = value;
                OnPropertyChanged();
            }
        }

	    private int _distance;
        public int Distance
        {
            get { return _distance; }
            set
            {
                if (value ==_distance) return;

                _distance = value;
                OnPropertyChanged();
            }
        }

        public TabPageMonitorViewModel()
	    {
            MessagingCenter.Subscribe<TabPageMonitor, Tuple<bool, int, double>>(this, "BeaconMessage", (sender, messageData) =>
            Device.BeginInvokeOnMainThread(() =>
            {
                ProcessBeaconMessage(messageData.Item1, messageData.Item2, messageData.Item3);
            }));
  
	    }

	    private void ProcessBeaconMessage(bool found, int rssi, double distance)
	    {
	        if (found)
	        {
	            // calculate the distance if its been calibrated
	            // default to the distance from library distance (aka accuracy)
	            distance = DistanceCalculator.Calibrated ? DistanceCalculator.CalculateDistance(rssi) : distance;

	            // set the message
	            Message = String.Format("(Rssi) {0} Distance {1:0.0}", rssi, distance);

	            // set the end ranges
	            Rssi = rssi;
	            Distance = Convert.ToInt32(distance*-1.0);
	        }
	        else
	        {
	            Message = "Searching...";
	        }
	    }
	}
}

