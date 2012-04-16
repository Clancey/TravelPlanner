using System;
using MonoTouch.Dialog;
namespace TravelPlanner
{
	public class TravelTickerMainController : DialogViewController
	{
		public TravelTickerMainController (bool pushing) 
			: base(new RootElement("Travel Ticket"), pushing)
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			var section = new Section()
			{
				new StringElement("Top Deals", () => {
					this.ActivateController(new TravelTickerController(Constants.TravelTickerUrl,"Top Deals",true));	
				})
			};
			
			this.Root.Add(section);
			
			this.TableView.BackgroundView = new BackGroundView(Theme.BackgroundImage,null,100);
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);			
			this.NavigationController.NavigationBar.TintColor = Theme.NavigationTint;
		}
	}
}

