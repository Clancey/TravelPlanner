using System;
using MonoTouch.Dialog;
using ClanceysLib;
using System.Globalization;

namespace TravelPlanner
{
	public partial class HotelSearchController
	{
		private EntryElement destElement;
		private CalendarElement startDateElement;
		private CalendarElement endDateElement;
		private ButtonElement searchButton;
		private ComboBoxElement roomsElement;
		private ComboBoxElement adultsElement;
		private ComboBoxElement childrenElement;
		
		
		void PopulateRoot()
		{

			destElement = new EntryElement("Destination","Address,Zip,City, or Airport","");
			//destElement.TextAlignment = UITextAlignment.Right;
			startDateElement = new CalendarElement("Check-in",DateTime.Today);
			startDateElement.closeOnSelect = true;
			startDateElement.OnDateSelected += delegate {
				if(startDateElement.DateValue >= endDateElement.DateValue)
				{
					endDateElement.DateValue = startDateElement.DateValue.AddDays(1);
					//endDateElement.Reload();
				}
			};
			
			endDateElement = new CalendarElement("Check-out",DateTime.Today.AddDays(1));
			endDateElement.closeOnSelect = true;
			searchButton = new ButtonElement("Find a hotel", Theme.IconColor, delegate{
				Search();
			});
			
			roomsElement = createRoomsElement();
			adultsElement = createAdultsElement();
			childrenElement = createChildrenElement();

			var sections = new Section[] {
				new Section() {
					destElement,
					startDateElement,
					endDateElement,
				},
				new Section("Room info") {
					roomsElement,
					adultsElement,
					childrenElement,
				},
				new Section() {
					searchButton,
				}
			};
			
			this.Root.Add(sections);
		}
	}
}

