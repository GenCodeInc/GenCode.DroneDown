using System;

namespace GenCode.Logging
{
	// follows System.Diagnostics.TraceLevel, sadly not avail in PCL so rolled my own enum
	public enum TraceLogLevel
	{
		Off = 0,
		Error,
		Info,
		Warning,
		Verbose
	}

	public static class Log
	{
		/// <summary>
		/// Custom Message Logging
		/// 
		/// Xamarin Team:
		/// 
		/// This is logging, basic, but wanted to show its important when debugging an error.
		/// Shows overloading, can pass a message or an exception.
		/// Shows default parameter, defaults to Error
		/// Shows Static classes and methods.
		/// 
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="traceLevel">Trace level.</param>
		public static void WriteLine(string message, TraceLogLevel traceLevel = TraceLogLevel.Error)
		{
			try {
				System.Diagnostics.Debug.WriteLine("[GenCodeLog]\n\tDateTime {0}\n\tTraceLevel {1}\n\t{2}", DateTime.Now, traceLevel, message);
				// Xamarin, Here we may do some other logging, like write it back to my service, parse or other persistant storage
			}
			catch {
				// If an exception is thrown during logging eat the error but should write it to console.
				// We sure would not want to blow up the app if writing to a persistant log like MSMQ/WCF/Db 
				// so we catch here, write, and never throw it back.
			}
		}

		/// <summary>
		/// Exception Logging, this will extract the message and the inner (if ! null) and send 
		/// it to the overloaded WriteLine logger that writes to persistant storage (above)
		/// </summary>
		/// <param name="ex">ex.</param>
		/// <param name="traceLevel">Trace level.</param>
		public static void WriteLine(Exception ex, TraceLogLevel traceLevel = TraceLogLevel.Error)
		{
			try {
				string message = String.Format ("{0} {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : "No Inner Message");
				WriteLine (message, traceLevel);
			}
			catch {
				// If an exception is thrown during logging eat the error but write it to console.
				// We sure would not want to blow up the app if writing to a log like MSMQ/WCF/Db 
				// or other storage for logging failed.
			}
		}
	}
}
