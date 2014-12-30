using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;

namespace GenCode.DroneDown.iOS
{

	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow _window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

		    _window = new UIWindow(UIScreen.MainScreen.Bounds) {RootViewController = App.GetMainPage().CreateViewController()};

		    _window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

