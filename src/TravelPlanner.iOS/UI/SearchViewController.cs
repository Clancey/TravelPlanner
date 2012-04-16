using System;
using MonoTouch.UIKit;
namespace TravelPlanner
{
	public class SearchViewController : UIViewController
	{
		public SearchViewController ()
		{
				
		}
		
		public override void ViewWillAppear (bool animated)
		{
			this.NavigationController.NavigationBar.TintColor = Theme.NavigationTint;
		}
		
	}
}

