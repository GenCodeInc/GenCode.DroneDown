using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace GenCode.DroneDown.ViewModels
{
	/// <summary>
	/// Monitor view model.
	/// </summary>
	class TabPageMonitorViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		string val;

		public TabPageMonitorViewModel ()
		{
			MyVal = DateTime.Now.ToString ();

			Device.StartTimer (TimeSpan.FromSeconds (1), () => {
				this.MyVal = DateTime.Now.ToString ();
				return true;
			});            
		}

		public string MyVal {
			set {
				if (val != value) {
					val = value;

					if (PropertyChanged != null) {
						PropertyChanged (this, 
							new PropertyChangedEventArgs ("MyVal"));
					}
				}
			}
			get {
				return val;
			}
		}
	}
}

