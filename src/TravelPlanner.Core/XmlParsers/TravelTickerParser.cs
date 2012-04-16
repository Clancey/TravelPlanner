using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using TravelPlanner.TravelTicker;
namespace TravelPlanner
{
	public static  class TravelTickerParser
	{
		public static TravelTickerSearchResults Parse(string xmlString)
		{
			TravelTickerSearchResults results = new TravelTickerSearchResults();
			var xmlDoc = XDocument.Parse(xmlString);
			var errors = xmlDoc.Descendants().Elements().Where(x=> x.Name == "Errors").FirstOrDefault().Value;
			if(!string.IsNullOrEmpty(errors))
			{
				Console.WriteLine(errors);
			}
			foreach(var element in  xmlDoc.Descendants("Result").Elements())
			{
				results.Result.Add(parseTravelTickerDeal(element));	
			}
			Console.WriteLine(errors);
			
			
			return results;
		}
		
			
		private static TravelTickerDeal parseTravelTickerDeal(XElement element)
		{
			var travelDeal = new TravelTickerDeal();
			travelDeal.Element = element;
			//travelDeal.Address = element.Element("Address").Value;
			//travelDeal.City = element.Element("City").Value;
			//travelDeal.Country = element.Element("Country").Value;
			travelDeal.DealOffers = parseDealOffers(element.Element("DealOffers"));
			//travelDeal.DestinationAirportCode = element.Element("DestinationAirportCode").Value;
			//travelDeal.DestinationCityName = element.Element("DestinationCityName").Value;
			//travelDeal.EncodedId = element.Element("EncodedId").Value;
			//travelDeal.ExpirationDate = element.Element("ExpirationDate").Value;
			//travelDeal.GoLiveDate = element.Element("GoLiveDate").Value;
			travelDeal.Images = parseImages(element.Element("Images"));
			travelDeal.ShortDetails = element.Element("ShortDetails").Value;
			//travelDeal.State = element.Element("State").Value;
			//travelDeal.SupplierName = element.Element("SupplierName").Value;
			//travelDeal.TaRatingCode = element.Element("TaRatingCode").Value;
			//travelDeal.Themes = parseThemes(element.Element("Themes"));
			travelDeal.Title = element.Element("Title").Value;
			travelDeal.URL = element.Element("URL").Value;
			if(!travelDeal.URL.ToLower().StartsWith("http://"))
				travelDeal.URL = "http://" + travelDeal.URL;
			//travelDeal.VerticalCode = element.Element("VerticalCode").Value;
			//travelDeal.ZipCode = element.Element("ZipCode").Value;
			return travelDeal;
		}
		
		public static void FillTravelTickerDeal(ref TravelTickerDeal travelDeal)
		{
			travelDeal.Address = travelDeal.Element.Element("Address").Value;
			travelDeal.City = travelDeal.Element.Element("City").Value;
			travelDeal.Country = travelDeal.Element.Element("Country").Value;
			travelDeal.DestinationAirportCode = travelDeal.Element.Element("DestinationAirportCode").Value;
			travelDeal.DestinationCityName = travelDeal.Element.Element("DestinationCityName").Value;
			travelDeal.EncodedId = travelDeal.Element.Element("EncodedId").Value;
			travelDeal.ExpirationDate = travelDeal.Element.Element("ExpirationDate").Value;
			travelDeal.GoLiveDate = travelDeal.Element.Element("GoLiveDate").Value;
			travelDeal.State = travelDeal.Element.Element("State").Value;
			travelDeal.SupplierName = travelDeal.Element.Element("SupplierName").Value;
			travelDeal.TaRatingCode = travelDeal.Element.Element("TaRatingCode").Value;
			travelDeal.Themes = parseThemes(travelDeal.Element.Element("Themes"));
			travelDeal.VerticalCode = travelDeal.Element.Element("VerticalCode").Value;
			travelDeal.ZipCode = travelDeal.Element.Element("ZipCode").Value;
		}
		#region DealOffers
		private static DealOffers parseDealOffers(XElement element)
		{
			var dealOffer = new DealOffers();
			foreach( var dealElement in element.Elements())
			{
				dealOffer.DealOffer.Add(parseDealOfferType(dealElement));
			}
			return dealOffer;
		}
		private static DealOfferType parseDealOfferType(XElement element)
		{
			var dealOffer = new DealOfferType();
			dealOffer.FromPrice = parseFromPriceType(element.Element("FromPrice"));
			dealOffer.PriceQualificationCode = element.Element("PriceQualificationCode").Value;
			dealOffer.Savings = element.Element("Savings").Value;
			dealOffer.SavingsTypeCode = element.Element("SavingsTypeCode").Value;
			dealOffer.ToPrice = parseToPriceType(element.Element("ToPrice"));
			dealOffer.ValidTravelDates = element.Element("ValidTravelDates").Value;
			return dealOffer;
		}
		private static FromPriceType parseFromPriceType(XElement element)			
		{
			var fromPrice = new FromPriceType();
			fromPrice.Amount = double.Parse(element.Element("Amount").Value);
			fromPrice.CurrencyCode = element.Element("CurrencyCode").Value;
			return fromPrice;
			
		}
		private static ToPriceType parseToPriceType(XElement element)			
		{
			var toPrice = new ToPriceType();
			toPrice.Amount = double.Parse(element.Element("Amount").Value);
			toPrice.CurrencyCode = element.Element("CurrencyCode").Value;
			return toPrice;
			
		}
		#endregion DealOffers
		private static List<string> parseImages(XElement element)
		{
			var images = new List<string>();
			foreach(var imageElement in element.Elements())
			{
				images.Add(imageElement.Value);	
			}
			return images;
		}
		#region Themes
		private static List<Theme> parseThemes(XElement element)
		{
			var themes = new List<Theme>();
			foreach(var themeElement in element.Elements())
			{
				themes.Add(parseTheme(themeElement));	
			}
			return themes;
		}
		private static Theme parseTheme(XElement element)
		{
			var theme = new Theme();
			theme.ThemeId = element.Element("ThemeId").Value;
			theme.ThemeName = element.Element("ThemeName").Value;
			return theme;
		}
		#endregion Themes
	}
}

