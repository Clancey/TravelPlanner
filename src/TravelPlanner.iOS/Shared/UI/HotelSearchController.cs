using System;
#if MONOTOUCH
using MonoTouch.Dialog;
using ClanceysLib;
#elif MONODROID
using MonoDroid.Dialog;
#endif
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
		
		
		void PopulateRoot ()
		{

			destElement = new EntryElement ("Destination", "Address,Zip,City, or Airport", "");
			//destElement.TextAlignment = UITextAlignment.Right;
			startDateElement = new CalendarElement ("Check-in", DateTime.Today);
			startDateElement.closeOnSelect = true;
			startDateElement.OnDateSelected += delegate {
				if (startDateElement.DateValue >= endDateElement.DateValue) {
					endDateElement.DateValue = startDateElement.DateValue.AddDays (1);
					//endDateElement.Reload();
				}
			};
			
			endDateElement = new CalendarElement ("Check-out", DateTime.Today.AddDays (1));
			endDateElement.closeOnSelect = true;
			searchButton = new ButtonElement ("Find a hotel", TravelPlanner.Theme.IconColor, delegate {
				Search ();
			});
			
			roomsElement = createRoomsElement ();
			adultsElement = createAdultsElement ();
			childrenElement = createChildrenElement ();

			var sections = new Section[] {
				new Section ("Search Details") {
					destElement,
					startDateElement,
					endDateElement,
				},
				new Section ("Room info") {
					roomsElement,
					adultsElement,
					childrenElement,
				},
				new Section () {
					searchButton,
				}
			};
			
			this.Root.Add (sections);
		}
	
		private string BuildSearchString ()
		{
			var searchString = String.Format (Constants.HotelSearchUrl,
												new object[]{
														destElement.Value
														, startDateElement.DateValue.ToString ("MM/dd/yyyy")
														, endDateElement.DateValue.ToString ("MM/dd/yyyy")
														, roomsElement.Value
														, adultsElement.Value
														, childrenElement.Value});
			return searchString;
			
		}
		private ComboBoxElement createRoomsElement()
		{
			var element = new ComboBoxElement("Rooms", new object[]{1,2,3,4,5,6},"");
			return element;
		}
		
		private ComboBoxElement createAdultsElement()
		{
			var element = new ComboBoxElement("Adults", new object[]{1,2,3,4},"");
			return element;
		}
		
		private ComboBoxElement createChildrenElement()
		{
			var element = new ComboBoxElement("Children", new object[]{0,1,2},"");
			return element;
		}
	}
}

