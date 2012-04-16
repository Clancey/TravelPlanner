using System;
using TravelPlanner.HotelSearch;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using System.Drawing;
using System.Globalization;
using ClanceysLib;
namespace TravelPlanner
{
	public partial class HotelDetailController : MyDialogViewController
	{
		float _padX = 5.0f;
		
		public HotelDetailController (HotelResult deal) : base (new RootElement("Details") ,true)
		{
			_deal = deal;
			
			
//			Root = new RootElement("Details")
//			{
//				new Section(header),
//				new Section()
//				{
//					new ButtonElement("Purchase",Theme.IconColor,delegate {
//						var web = new WebViewController();
//						web.OpenUrl(this,_deal.Url,_deal.Title);	
//					})	
//				},
//				detailsSection,
//				amenities,
//			};
//			Root.Add(new Section("Aditional Information") {
//				averagepernight,
//				rooms,
//				nights,
//				taxes,
//				totalPrice,
//			});
			Root.UnevenRows = true;
			TableView.BackgroundView = new BackGroundView(Theme.BackgroundImage,null,100);
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			PopulateRoot();
		}
		
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}
		private void purchase()
		{
			var web = new WebViewController();
			web.OpenUrl(this, _deal.Url, _deal.Title);
		}
		private void showMap()
		{
			var mapVC = new MapViewController(_deal.Neighborhood.Id);
			this.ActivateController(mapVC);
		}
	}
}

