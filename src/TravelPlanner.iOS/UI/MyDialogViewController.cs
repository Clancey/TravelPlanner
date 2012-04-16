using System;
using MonoTouch.Dialog;
namespace TravelPlanner
{
	public class MyDialogViewController : DialogViewController
	{
		public MyDialogViewController (RootElement root,bool pushing) : base (root,pushing)
		{
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);			
			//this.SearchBarTintColor = UITheme.NavigationTint;
			this.NavigationController.NavigationBar.TintColor = Theme.NavigationTint;
		}
	}
}

