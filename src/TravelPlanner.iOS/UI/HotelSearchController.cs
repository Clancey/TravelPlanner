using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
using ClanceysLib;
namespace TravelPlanner
{
	public partial class HotelSearchController : MyDialogViewController
	{
		public HotelSearchController () : base(new RootElement("Search Hotels"),false)
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			PopulateRoot();
			TableView.BackgroundView = new BackGroundView(Theme.BackgroundImage,null,100);
			
		}
		private void Search()
		{
			destElement.BecomeFirstResponder(false);
			destElement.ResignFirstResponder(false);
			
			destElement.FetchValue();
			if(string.IsNullOrEmpty(destElement.Value))
			{
				var alert = new UIAlertView("Error","Please enter a destination",null,"Ok");
				alert.Clicked += delegate {
					destElement.BecomeFirstResponder(true);
				};
				alert.Show();
				return;
			}
			this.ActivateController(new HotelViewController(BuildSearchString()));
		}
	}
}

