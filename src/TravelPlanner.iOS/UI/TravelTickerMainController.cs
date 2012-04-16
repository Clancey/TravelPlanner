using System;
using MonoTouch.Dialog;
namespace TravelPlanner
{
	public partial class TravelTickerMainController : DialogViewController
	{
		public TravelTickerMainController (bool pushing) 
			: base(new RootElement("Travel Ticket"), pushing)
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			PopulateRoot();
			this.TableView.BackgroundView = new BackGroundView(Theme.BackgroundImage,null,100);
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);			
			this.NavigationController.NavigationBar.TintColor = Theme.NavigationTint;
		}

		private void ShowDeals(string url, string title)
		{
			this.ActivateController(new TravelTickerController(url,title,true));	
		}
	}
}

