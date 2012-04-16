
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog.Utilities;

namespace TravelPlanner
{
	public class Application
	{
		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}
	}

	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		public RotatingTabBar TabBar;
		public UINavigationController [] navigationRoots;
		HotelSearchController hotelVC;
		TravelTickerController travelTickerVC;
		CarSearchController carVC;
		// This method is invoked when the application has loaded its UI and its ready to run
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow(UIScreen.MainScreen.Bounds);
			TabBar = new RotatingTabBar(2);
			hotelVC = new  HotelSearchController();
			travelTickerVC = new TravelTickerController(Constants.TravelTickerUrl,"Travel-Ticker",true);
			carVC = new CarSearchController();
			navigationRoots = new UINavigationController[3] {
				
				new UINavigationController (hotelVC) {
					TabBarItem = new UITabBarItem ("Hotels", UIImage.FromBundle ("search.png"), 0)
				},
				new UINavigationController (carVC) {
					TabBarItem = new UITabBarItem ("Car Rentals", UIImage.FromBundle ("Images/car_icon.png"), 1)
				},
				new UINavigationController (travelTickerVC) {
					TabBarItem = new UITabBarItem ("Travel Ticker", UIImage.FromBundle ("news.png"), 2)
				},
				
			};
			TabBar.SetViewControllers (navigationRoots, false);
			
			window.AddSubview(TabBar.View);
			
			//ImageLoader.DeleteOldFiles(DateTime.Now.AddDays(-1));
			
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

