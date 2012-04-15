using System;
using MonoTouch.UIKit;
namespace Hotwire
{
	public class SearchViewController : UIViewController
	{
		public SearchViewController ()
		{
				
		}
		
		public override void ViewWillAppear (bool animated)
		{
			this.NavigationController.NavigationBar.TintColor = UITheme.NavigationTint;
		}
		
	}
}

