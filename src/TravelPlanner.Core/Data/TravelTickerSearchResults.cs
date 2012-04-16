using System;
using System.Collections.Generic;
using System.Xml.Linq;
namespace TravelPlanner.TravelTicker
{
	public class TravelTickerSearchResults
	{
		public TravelTickerSearchResults()
		{	
			Result = new List<TravelTickerDeal>();
		}
		/// <remarks/>
		public string Errors {get;set;}

		/// <remarks/>
		public List<TravelTickerDeal> Result {get;set;}
		
	}
	
	public partial class TravelTickerDeal
	{
		public XElement Element{get;set;}
		private string addressField;

		private string cityField;

		private string countryField;

		private DealOffers dealOffersField;

		private string verticalCodeField;

		private string destinationCityNameField;

		private string destinationAirportCodeField;

		private string encodedIdField;

		private string expirationDateField;

		private string goLiveDateField;

		private string[] imagesField;

		private string uRLField;

		private string shortDetailsField;

		private string stateField;

		private string supplierNameField;

		private string taRatingCodeField;

		private Theme[] themesField;

		private string titleField;

		private string zipCodeField;

		/// <remarks/>
		public string Address {get;set;}

		/// <remarks/>
		public string City {get;set;}

		/// <remarks/>
		public string Country {
			get { return this.countryField; }
			set { this.countryField = value; }
		}

		/// <remarks/>
		public DealOffers DealOffers {
			get { return this.dealOffersField; }
			set { this.dealOffersField = value; }
		}

		/// <remarks/>
		public string VerticalCode {
			get { return this.verticalCodeField; }
			set { this.verticalCodeField = value; }
		}

		/// <remarks/>
		public string DestinationCityName {
			get { return this.destinationCityNameField; }
			set { this.destinationCityNameField = value; }
		}

		/// <remarks/>
		public string DestinationAirportCode {
			get { return this.destinationAirportCodeField; }
			set { this.destinationAirportCodeField = value; }
		}

		/// <remarks/>
		public string EncodedId {
			get { return this.encodedIdField; }
			set { this.encodedIdField = value; }
		}

		/// <remarks/>
		public string ExpirationDate {
			get { return this.expirationDateField; }
			set { this.expirationDateField = value; }
		}

		/// <remarks/>
		public string GoLiveDate {
			get { return this.goLiveDateField; }
			set { this.goLiveDateField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("ImageURL", IsNullable = false)]
		public List<string> Images {get;set;}

		/// <remarks/>
		public string URL {
			get { return this.uRLField; }
			set { this.uRLField = value; }
		}

		/// <remarks/>
		public string ShortDetails {
			get { return this.shortDetailsField; }
			set { this.shortDetailsField = value; }
		}

		/// <remarks/>
		public string State {
			get { return this.stateField; }
			set { this.stateField = value; }
		}

		/// <remarks/>
		public string SupplierName {
			get { return this.supplierNameField; }
			set { this.supplierNameField = value; }
		}

		/// <remarks/>
		public string TaRatingCode {get;set;}
		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Theme", IsNullable = false)]
		public List<Theme> Themes {get;set;}

		/// <remarks/>
		public string Title {
			get { return this.titleField; }
			set { this.titleField = value; }
		}

		/// <remarks/>
		public string ZipCode {get;set;}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class DealOffers
	{
		public DealOffers()
		{
			DealOffer = new List<DealOfferType>();
		}
		/// <remarks/>
		public List<DealOfferType> DealOffer {get;set;}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class DealOfferType
	{

		private FromPriceType fromPriceField;

		private string priceQualificationCodeField;

		private string savingsField;

		private string savingsTypeCodeField;

		private ToPriceType toPriceField;

		private string validTravelDatesField;

		/// <remarks/>
		public FromPriceType FromPrice {
			get { return this.fromPriceField; }
			set { this.fromPriceField = value; }
		}

		/// <remarks/>
		public string PriceQualificationCode {
			get { return this.priceQualificationCodeField; }
			set { this.priceQualificationCodeField = value; }
		}

		/// <remarks/>
		public string Savings {
			get { return this.savingsField; }
			set { this.savingsField = value; }
		}

		/// <remarks/>
		public string SavingsTypeCode {
			get { return this.savingsTypeCodeField; }
			set { this.savingsTypeCodeField = value; }
		}

		/// <remarks/>
		public ToPriceType ToPrice {
			get { return this.toPriceField; }
			set { this.toPriceField = value; }
		}

		/// <remarks/>
		public string ValidTravelDates {
			get { return this.validTravelDatesField; }
			set { this.validTravelDatesField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class FromPriceType
	{
		/// <remarks/>
		public double Amount {get;set;}

		/// <remarks/>
		public string CurrencyCode {get;set;}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class Theme
	{

		/// <remarks/>
		public string ThemeId {get;set;}

		/// <remarks/>
		public string ThemeName {get;set;}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public enum ThemeTypeThemeId
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("1")]
		Item1,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("2")]
		Item2,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("3")]
		Item3,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("4")]
		Item4,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("5")]
		Item5,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("6")]
		Item6,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("7")]
		Item7,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("8")]
		Item8,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("9")]
		Item9,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("10")]
		Item10,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("11")]
		Item11,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("12")]
		Item12,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("50")]
		Item50,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("13")]
		Item13,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("14")]
		Item14,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("15")]
		Item15,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("16")]
		Item16,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("17")]
		Item17,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("18")]
		Item18,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("19")]
		Item19,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("20")]
		Item20,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("21")]
		Item21,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("22")]
		Item22,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("23")]
		Item23,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("24")]
		Item24,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("25")]
		Item25,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("26")]
		Item26,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("27")]
		Item27
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public enum ThemeTypeThemeName
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Thanksgiving Trips")]
		ThanksgivingTrips,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Holiday Getaways")]
		HolidayGetaways,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Away for MLK")]
		AwayforMLK,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Valentine Vacations")]
		ValentineVacations,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Presidents Day Weekend")]
		PresidentsDayWeekend,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Spring Break Bargains")]
		SpringBreakBargains,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Memorial Day Weekend")]
		MemorialDayWeekend,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Fourth of July Favorites")]
		FourthofJulyFavorites,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Labor Day Away")]
		LaborDayAway,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Weekend Getaway")]
		WeekendGetaway,

		/// <remarks/>
		Mexico,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("All Inclusive")]
		AllInclusive,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Hip and Trendy")]
		HipandTrendy,

		/// <remarks/>
		Europe,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Sun and Beach")]
		SunandBeach,

		/// <remarks/>
		Caribbean,

		/// <remarks/>
		International,

		/// <remarks/>
		Romance,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Winter and Ski")]
		WinterandSki,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Resorts and Spas")]
		ResortsandSpas,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Big City Getaways")]
		BigCityGetaways,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Hotels Under a Hundred")]
		HotelsUnderaHundred,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Go Green")]
		GoGreen,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Adventure Travel")]
		AdventureTravel,

		/// <remarks/>
		Luxury,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Family Travel")]
		FamilyTravel,

		/// <remarks/>
		Exotic,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("Early Show")]
		EarlyShow
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class ToPriceType
	{
		/// <remarks/>
		public double Amount {get;set;}
		/// <remarks/>
		public string CurrencyCode {get;set;}
	}
}

