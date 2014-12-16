using System;
using Xamarin.Forms;

namespace GenCode.DroneDown
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			return new DroneDown.MainContent ();
		}
	}
}

