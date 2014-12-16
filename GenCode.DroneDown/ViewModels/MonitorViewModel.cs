using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace GenCode.DroneDown.ViewModels
{

	/// <summary>
	/// Monitor view model.
	/// </summary>
	class MonitorViewModel : INotifyPropertyChanged
	{
		string val;

		public event PropertyChangedEventHandler PropertyChanged;

		public MonitorViewModel()
		{
			this.MyVal = DateTime.Now.ToString();

			Device.StartTimer(TimeSpan.FromSeconds(1), () => {
					this.MyVal = DateTime.Now.ToString();
					return true;
				});            
		}

		public string MyVal
		{
			set
			{
				if (val != value)
				{
					val = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, 
							new PropertyChangedEventArgs("MyVal"));
					}
				}
			}
			get
			{
				return val;
			}
		}
	}
}

