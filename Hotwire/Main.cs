
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ClanceysLib;
using MonoTouch.Dialog.Utilities;

namespace Hotwire
{
	public class Application
	{
		static void Main (string[] args)
		{
			UIApplication.Main (args);
		}
	}

	// The name AppDelegate is referenced in the MainWindow.xib file.
	public partial class AppDelegate : UIApplicationDelegate
	{
		
		public RotatingTabBar TabBar;
		public UINavigationController [] navigationRoots;
		HotelSearchDialogViewController hotelVC;
		TravelTickerMainViewController travelTickerVC;
		CarSearchDialogViewController carVC;
		// This method is invoked when the application has loaded its UI and its ready to run
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// If you have defined a view, add it here:
			// window.AddSubview (navigationController.View);
			
			TabBar = new RotatingTabBar(2);
			hotelVC = new  HotelSearchDialogViewController();
			travelTickerVC = new TravelTickerMainViewController(false);
			carVC = new CarSearchDialogViewController();
			navigationRoots = new UINavigationController[3]{
				new UINavigationController (hotelVC) {
					TabBarItem = new UITabBarItem ("Hotels", UIImage.FromBundle ("search.png"), 0)
				},
				new UINavigationController (carVC) {
					TabBarItem = new UITabBarItem ("Car Rentals", UIImage.FromBundle ("Images/car_icon.png"), 1)
				},
				new UINavigationController (travelTickerVC) {
					TabBarItem = new UITabBarItem ("Travel Ticker", UIImage.FromBundle ("news.png"),2)
				},
			};
			TabBar.SetViewControllers (navigationRoots, false);
			
			window.AddSubview(TabBar.View);
			
			//ImageLoader.DeleteOldFiles(DateTime.Now.AddDays(-1));
			
			
			window.MakeKeyAndVisible ();
			
			return true;
		}

		// This method is required in iPhoneOS 3.0
		public override void OnActivated (UIApplication application)
		{
		}
	}
}

