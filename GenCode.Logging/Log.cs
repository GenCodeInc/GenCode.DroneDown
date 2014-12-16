using System;

namespace GenCode.Logging
{
	// follows System.Diagnostics.TraceLevel, sadly not avail in PCL so rolled my own
	public enum TraceLevel
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
		public static void WriteLine(string message, TraceLevel traceLevel = TraceLevel.Error)
		{
			System.Diagnostics.Debug.WriteLine(String.Format("{0} \nTraceLevel {1}", message, traceLevel));

			//  Xamarin, Here we may do some other logging, like write it back to my service, parse or whatever
		}

		/// <summary>
		/// Exception Logging
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="traceLevel">Trace level.</param>
		public static void WriteLine(Exception message, TraceLevel traceLevel = TraceLevel.Error)
		{
			System.Diagnostics.Debug.WriteLine(String.Format("Message {0} \nTraceLevel {1}", message.Message, traceLevel));

			//  Xamarin, Here we may do some other logging, like write it back to my service, parse or whatever
		}
	}
}
