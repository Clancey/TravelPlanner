using System;
using TravelPlanner.HotelSearch;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using System.Drawing;
using System.Globalization;
using ClanceysLib;
namespace TravelPlanner
{
	public class HotelDetailController : MyDialogViewController
	{
		HotelResult _deal;
		float _padX = 5.0f;
		
		public HotelDetailController (HotelResult deal) : base (new RootElement("Details") ,true)
		{
			_deal = deal;
			
			var header = new HotelHeader( new RectangleF (_padX, 0, View.Bounds.Width-30-_padX*2, 100),deal);
			
			var detailsSection = new Section("Details");
			
			var description =
				new MultilineElement("",_deal.Neighborhood.Description);
			if(!string.IsNullOrEmpty(_deal.Neighborhood.Description))
				detailsSection.Add(description);
			
			
			var mapElement = new StringElement("Map",delegate {
				var mapVC = new MapViewController(deal.Neighborhood.Id);
				this.ActivateController(mapVC);
			});
			detailsSection.Add(mapElement);

			var amenities = new Section("Amenities");
			foreach(var amenity in deal.Amenties)
				amenities.Add(new HotelAmenityElement(amenity));
			var paidAmenities = new Section("Paid amenities");
			foreach(var amenity in deal.PaidAmenities)
				paidAmenities.Add(new HotelAmenityElement(amenity));
			Root = new RootElement("Details")
			{
				new Section(header),
				new Section()
				{
					new ButtonElement("Purchase",Theme.IconColor,delegate {
						var web = new WebViewController();
						web.OpenUrl(this,_deal.Url,_deal.Title);	
					})	
				},
				detailsSection,
				amenities,
			};
			if(paidAmenities.Count > 0)
				Root.Add(paidAmenities);
			
			var averagepernight = new StringElement("Average per night",deal.AveragePricePerNight.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")));			
			var taxes = new StringElement("Taxes and Fees",deal.TaxesAndFees.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")));
			var totalPrice = new StringElement("Total Price",deal.TotalPrice.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")));
			
			
			var rooms = new StringElement("Rooms",deal.Rooms.ToString());
			var nights = new StringElement("Nights",deal.Nights.ToString());

			
			Root.Add(new Section("Aditional Information") {
				averagepernight,
				rooms,
				nights,
				taxes,
				totalPrice,
			});
			
			TableView.BackgroundView = new BackGroundView(Theme.BackgroundImage,null,100);
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			

		}
		
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}
		
		private class HotelHeader : UIView
		{
			public DealView dealView;
			public UITextView Title;
			public HotelHeader(RectangleF rect, HotelResult deal) : base (rect)
			{
				dealView = new DealView(new RectangleF (10, 10, 73, 73),deal);
				dealView.Price.Font = UIFont.BoldSystemFontOfSize(25f);
				int TextX = 95;
				var w = rect.Width-TextX;
				Title = new UITextView(new RectangleF (TextX, 12, w, 75)){Text = deal.Title,Font = UIFont.BoldSystemFontOfSize (19)};//{, userFont, UILineBreakMode.TailTruncation
				Title.BackgroundColor = UIColor.Clear;
				this.AddSubview(dealView);
				this.AddSubview(Title);
			}
		}
	}
}

