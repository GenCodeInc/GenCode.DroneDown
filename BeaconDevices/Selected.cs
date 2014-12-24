using System;
using GenCode.BeaconDevices.Manufacturers;
using GenCode.Logging;

namespace GenCode.BeaconDevices
{
	/// <summary>
	/// Singelton class used to select a beacon device and use by Forms/Android and iOS
	/// </summary>

	public static class Default
	{
		private static Device _default;

		public static Device GetDevice {
			get {
				return _default;
			}
			set {
				_default = value;
				Log.WriteLine (String.Format ("User has selected {0}", _default), TraceLogLevel.Verbose);
			}
		}
	}
}

