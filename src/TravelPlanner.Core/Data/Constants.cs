using System;
namespace TravelPlanner
{
	public static class Constants
	{
		public static string ApiKey = "jg94e4du6dcae5uqwfuhmm9p";
		private static string staticParams = "?linkshareid=BJaziy6Rv6A&apikey=" + ApiKey;// + "&format=JSON";
		
		public static string HotelUrl = "http://api.hotwire.com/v1/search/hotel" + staticParams;
		
		public static string CarUrl = "http://api.hotwire.com/v1/search/car?" + staticParams;
		
		public static string TravelTickerUrl = "http://api.hotwire.com/v1/deal/travel-ticker" + staticParams;
		//public static string TravelTickerUrl = "http://thecollaborateapp.com/ws/hotwire/SearchHotWireTravelTicker.php";
		public static string ThemesUrl = "http://api.hotwire.com/v1/meta/travel-ticker?apikey=" + ApiKey;
		private static string travelTickerDealUrl = "http://www.travel-ticker.com/supplier-forward?dealId=";
		
		public static string GetTravelDealUrl(string encodedId)
		{
			return travelTickerDealUrl + encodedId + "&linkshareid=BJaziy6Rv6A";
		}
		public static string HotelSearchUrl = HotelUrl + "&dest={0}&startdate={1}&enddate={2}&rooms={3}&adults={4}&children={5}";
	}
}

