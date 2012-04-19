// 
//  Copyright 2012  James Clancey, Xamarin Inc  (http://www.xamarin.com)
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System;
using System.Drawing;
using TravelPlanner.HotelSearch;


#if MONOTOUCH
using MonoTouch.Dialog;
using ClanceysLib;
#elif MONODROID
using MonoDroid.Dialog;
#endif
using System.Globalization;
namespace TravelPlanner
{
	public partial class HotelDetailController
	{
		
		HotelResult _deal;
		private void PopulateRoot()
		{			
			var headerSection = new Section(){
				new HotelResultElement(_deal,false,null)
			};
			
			var buttonElementSection = new Section() {
				new ButtonElement("Purchase", TravelPlanner.Theme.IconColor, delegate {
					purchase();	
				}) 
			};
			
			var detailsSection = new Section("Details");

			if(string.IsNullOrWhiteSpace(_deal.Neighborhood.Description) == false) {
				Console.WriteLine(_deal.Neighborhood.Description);
				detailsSection.Add(new StringElement(_deal.Neighborhood.Description,""));
			}
			
			var mapElement = new StringElement("Map",delegate {
				showMap();
			});
			
			detailsSection.Add(mapElement);

			var amenities = new Section("Amenities");
			foreach(var amenity in _deal.Amenties)
				amenities.Add(new HotelAmenityElement(amenity));
			
			var paidAmenities = new Section("Paid amenities");
			foreach(var amenity in _deal.PaidAmenities)
				paidAmenities.Add(new HotelAmenityElement(amenity));
			
			if(paidAmenities.Count > 0)
				Root.Add(paidAmenities);
			
			var averagepernight = new StringElement("Average per night", _deal.AveragePricePerNight.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")));			
			var taxes = new StringElement("Taxes and Fees", _deal.TaxesAndFees.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")));
			var totalPrice = new StringElement("Total Price", _deal.TotalPrice.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")));
			
			
			var rooms = new StringElement("Rooms", _deal.Rooms.ToString());
			var nights = new StringElement("Nights", _deal.Nights.ToString());

			var additionalInfoSection = new Section("Aditional Information") {
				averagepernight,
				rooms,
				nights,
				taxes,
				totalPrice,
			};
			
			
			Root.Add(new Section[] {
				headerSection,
				buttonElementSection,
				detailsSection,
				amenities,
				additionalInfoSection,
				
			});
			
			
			
			
		}
	}
}

