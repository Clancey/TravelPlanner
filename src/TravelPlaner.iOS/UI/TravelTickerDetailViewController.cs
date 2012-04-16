using System;
using TravelPlaner.TravelTicker;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using System.Drawing;
using System.Linq;
using ClanceysLib;
namespace TravelPlaner
{
	public class TravelTickerDetailViewController : MyDialogViewController 
	{
		const int PadX = 4;
		TravelTickerDeal Deal;
		
		public TravelTickerDetailViewController (TravelTickerDeal deal) : base (null, true)
		{
			Style = UITableViewStyle.Grouped;
			this.Title = "Details";
			//this.tableView.BackgroundColor = UIColor.Clear;
			Deal = deal;
			TravelTickerParser.FillTravelTickerDeal(ref Deal);
			CreateUI ();			
			TableView.BackgroundView = new BackGroundView(UITheme.BackgroundImage,null,100);
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewDidAppear(animated);
			
			//this.tableView.BackgroundColor = UIColor.FromPatternImage(Images.UIStockImageUnderPageBackground);
		}
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear(animated);
		}
		
		void CreateUI ()
		{
			var profileRect = new RectangleF (PadX, 0, View.Bounds.Width-30-PadX*2, 100);
			var shortProfileView = new ShortProfileView (profileRect, Deal.Images.Last(),Deal.Title);
			var purchaseSection = new Section()
			{
				new ButtonElement("View on Travel-Ticker",UITheme.IconColor,delegate {
					var web = new WebViewController();
					web.OpenUrl(this,Constants.GetTravelDealUrl(Deal.EncodedId),Deal.Title);	
				})
			};

			var main = new Section (shortProfileView){
			};
			var detailsElement = 
				new MultilineElement("",Deal.ShortDetails);
			//detailsElement.Font = UIFont.SystemFontOfSize(17f);
			
			var summarySection = new Section("Summary")
			{
				detailsElement
			};
			var dealOffer = Deal.DealOffers.DealOffer[0];
			var startPrice = dealOffer.FromPrice;
			var endPrice = dealOffer.ToPrice;
			string price = "";
			if(startPrice.Amount == endPrice.Amount)
				price = startPrice.Amount.ToString("C0");
			else
				price = startPrice.Amount.ToString("C0");
			var priceElement = new StringElement("Price",price + " " + Deal.DealOffers.DealOffer[0].PriceQualificationCode);
			string savings = "";
			if(dealOffer.SavingsTypeCode == "%")
				savings = dealOffer.Savings + dealOffer.SavingsTypeCode;
			else
				savings = dealOffer.SavingsTypeCode + dealOffer.Savings;
			var savingsElement = new MultilineElement("Savings",savings);
			var expirationElement = new MultilineElement("Expires",Deal.ExpirationDate);
			var validTravelDatesElement = new MultilineElement("Travel Dates", dealOffer.ValidTravelDates);
			var detailsSection = new Section ("Details") ;
			
			detailsSection.Add(priceElement);
			if(!string.IsNullOrEmpty(dealOffer.Savings))
				detailsSection.Add(savingsElement);
			detailsSection.Add(validTravelDatesElement);
			detailsSection.Add(expirationElement);
			
			Root = new RootElement (Deal.Title){
				main,
				purchaseSection,
				detailsSection,
				summarySection,
				TravelDetails(),
				Themes(),
			};

		}

		public Section Themes()
		{
			var section = new Section("Deal Themes");
			foreach(var theme in Deal.Themes)
			{
				section.Add(new TravelTickerThemeElement(theme));	
			}
			return section;
		}
		
		public Section TravelDetails()
		{
			var section = new Section("Destination");
			
			section.Add(new StringElement("City", Deal.DestinationCityName));
			section.Add(new StringElement("Airport",Deal.DestinationAirportCode));
			return section;
		}
		
	}
		
}

