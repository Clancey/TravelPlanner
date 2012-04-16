using System;
using TravelPlaner.HotelSearch;
using System.Xml.Linq;
using System.Linq;
#if Monotouch
using MonoTouch.CoreLocation;
#endif
namespace TravelPlaner
{
	public class HotelSearchParser
	{
		public static HotelSearchResults results;
		public static HotelSearchResults Parse(string xmlString)
		{
			results = new HotelSearchResults();
			var xmlDoc = XDocument.Parse(xmlString);
			var errors = xmlDoc.Descendants().Elements().Where(x=> x.Name == "Errors").FirstOrDefault().Value;
			if(!string.IsNullOrEmpty(errors))
				Console.WriteLine(errors);
			foreach(var element  in xmlDoc.Descendants().Elements("Amenities").Elements())
				results.Amenities.Add(parseAmenity(element));
			foreach(var element in xmlDoc.Descendants("Neighborhoods").Elements())
				results.Neighborhoods.Add(parseNeighborhood(element));
			
			foreach(var element in  xmlDoc.Descendants("Result").Elements())
				results.Results.Add(parseHotelResult(element));	

			Console.WriteLine(errors);
			return results;
		}
		
		private static Amenity parseAmenity (XElement element)
		{
			var amenity = new Amenity();
			amenity.Code = element.Element("Code").Value;
			amenity.Description = element.Element("Description").Value;
			amenity.Name = element.Element("Name").Value;
			return amenity;
		}
		
		private static Neighborhood parseNeighborhood (XElement element)
		{
			var neighborhood = new Neighborhood();
			neighborhood.Centeroid = parseLatLong(element.Element("Centroid"));
			neighborhood.City = element.Element("City").Value;
			neighborhood.Country = element.Element("Country").Value;
			var description = element.Element("Description");
			if(description != null)
				neighborhood.Description = description.Value;
			neighborhood.Id = element.Element("Id").Value;
			neighborhood.Name = element.Element("Name").Value;
			neighborhood.State = element.Element("State").Value;
			foreach(var shapeElement in element.Element("Shape").Elements())
				neighborhood.Shape.Add(parseLatLong(shapeElement));
			return neighborhood;
		}
		
		private static Coordinate parseLatLong (XElement element)
		{
			string[] values = element.Value.Split(new char[]{char.Parse(",")});
			return new Coordinate(double.Parse(values[0]),double.Parse(values[1]));
		}
		
		private static HotelResult parseHotelResult (XElement element)
		{
			var hotelResult = new HotelResult();
			foreach(var amenityElement in element.Element("AmenityCodes").Elements())
				hotelResult.Amenties.Add(results.Amenities.Where(x=> x.Code == amenityElement.Value).FirstOrDefault());
			hotelResult.AveragePricePerNight = double.Parse(element.Element("AveragePricePerNight").Value);
			hotelResult.CheckInDate = DateTime.Parse(element.Element("CheckInDate").Value);			
			hotelResult.CheckOutDate = DateTime.Parse(element.Element("CheckOutDate").Value);
			hotelResult.CurrencyCode = element.Element("CurrencyCode").Value;
			hotelResult.HWRefNumber = element.Element("HWRefNumber").Value;
			hotelResult.LodgingTypeCode = element.Element("LodgingTypeCode").Value;
			hotelResult.Neighborhood = results.Neighborhoods.Where(x=> x.Id == element.Element("NeighborhoodId").Value).FirstOrDefault();
			hotelResult.Nights = int.Parse(element.Element("Nights").Value);
			foreach(var amenityElement in element.Element("PaidAmenities").Elements())
				hotelResult.PaidAmenities.Add(results.Amenities.Where(x=> x.Code == amenityElement.Value).FirstOrDefault());
			hotelResult.Rooms = int.Parse(element.Element("Rooms").Value);
			hotelResult.StarRating = double.Parse(element.Element("StarRating").Value);
			hotelResult.SubTotal = double.Parse(element.Element("SubTotal").Value);
			hotelResult.TaxesAndFees = double.Parse(element.Element("TaxesAndFees").Value);
			hotelResult.TotalPrice = double.Parse(element.Element("TotalPrice").Value);
			hotelResult.Url = element.Element("LinkshareDeepLink").Value;
			
			return hotelResult;
		}
		
	}
}

