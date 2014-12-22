using System;
using GenCode.BeaconDevices.Manufacturers;

namespace GenCode.BeaconDevices
{
	/// <summary>
	/// Singelton class used to select a beacon device and use by Forms/Android and iOS
	/// </summary>
	public static class Default {
		public static Device Device {
			get;
			set;
		}
	}
}

