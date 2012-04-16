using System;
using System.Collections.Generic;
#if MONOTOUCH
using MonoTouch.CoreLocation;
#endif
namespace TravelPlaner.HotelSearch
{
	public class HotelSearchResults
	{		
		public List<HotelResult> Results {get;set;}
		public List<Neighborhood> Neighborhoods {get;set;}
		public List<Amenity> Amenities {get;set;}
		public HotelSearchResults ()
		{
			Results = new List<HotelResult>();
			Amenities = new List<Amenity>();
			Neighborhoods = new List<Neighborhood>();
		}
	}
		
	public class HotelResult
	{
		public HotelResult()
		{
			Amenties = new List<Amenity>();	
			PaidAmenities = new List<Amenity>();
		}
		public string CurrencyCode {get;set;}
		public string Url {get;set;}
		public string ResultId {get;set;}
		public string HWRefNumber {get;set;}
		public double SubTotal {get;set;}
		public double TaxesAndFees {get;set;}
		public double TotalPrice {get;set;}
		public List<Amenity> Amenties {get;set;}
		public DateTime CheckInDate {get;set;}
		public DateTime CheckOutDate {get;set;}
		public Neighborhood Neighborhood {get;set;}
		public string LodgingTypeCode {get;set;}
		public int Nights {get;set;}
		public List<Amenity> PaidAmenities {get;set;}
		public double AveragePricePerNight {get;set;}
		public int Rooms {get;set;}
		public double StarRating {get;set;}
		public string LogingType {
			get {switch(LodgingTypeCode){
					case "H":
				return "hotel";
					case "C":
					return "condo";
					case "A":
					return "all-inclusive resort";
					default: return "";
				}
		}
	}
		
		public string Title {
			get { var title = Neighborhood.Name + " area " + LogingType;
				return title;
			}
		}
	}
	
	public class Neighborhood
	{
		public Neighborhood()
		{
			Shape = new List<Coordinate>();	
		}
		public string Id {get;set;}
		public string Name {get;set;}
		public Coordinate Centeroid {get;set;}
		public string City {get;set;}
		public string State {get;set;}
		public string Country {get;set;}
		public string Description {get;set;}
		public List<Coordinate> Shape {get;set;}
		
	}
	
	public class Amenity
	{
		public string Code{get;set;}
		public string Description{get;set;}
		public string Name {get;set;}		
	}
	
}

