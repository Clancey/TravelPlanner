using System;
using MonoTouch.Dialog;
namespace TravelPlaner
{
	public class TravelTickerMainViewController : DialogViewController
	{
		public TravelTickerMainViewController (bool pushing) : base(null,pushing)
		{
			var section = new Section()
			{
				new StringElement("Top Deals",delegate {
					this.ActivateController(new TravelTickerViewController(Constants.TravelTickerUrl,"Top Deals",true));	
				})
			};
			Root = new RootElement("Travel-Ticker")
			{
				section	
			};
			this.TableView.BackgroundView = new BackGroundView(UITheme.BackgroundImage,null,100);
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);			
			this.NavigationController.NavigationBar.TintColor = UITheme.NavigationTint;
		}
	}
}

