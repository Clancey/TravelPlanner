using System;
using MonoTouch.Dialog;
using ClanceysLib;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
namespace Hotwire
{
	public class HotelSearchDialogViewController : MyDialogViewController
	{
		private EntryElement destElement;
		private DateElement startDateElement;
		private DateElement endDateElement;
		private ButtonElement searchButton;
		private ComboBoxElement roomsElement;
		private ComboBoxElement adultsElement;
		private ComboBoxElement childrenElement;
		
		public HotelSearchDialogViewController () : base(null,false)
		{
			destElement = new EntryElement("Destination","Address,Zip,City, or Airport","");
			destElement.TextAlignment = UITextAlignment.Right;
			startDateElement = new DateElement("Check-in",DateTime.Today);
			startDateElement.closeOnSelect = true;
			startDateElement.OnDateSelected += delegate{
				if(startDateElement.DateValue >= endDateElement.DateValue)
				{
					endDateElement.DateValue = startDateElement.DateValue.AddDays(1);
					endDateElement.Reload();
					
					
				}
			};
			endDateElement = new DateElement("Check-out",DateTime.Today.AddDays(1));
			endDateElement.closeOnSelect = true;
			searchButton = new ButtonElement("Find a hotel",UITheme.IconColor, delegate{
				Search();
			});
			
			roomsElement = createRoomsElement();
			adultsElement = createAdultsElement();
			childrenElement = createChildrenElement();
			
			Root = new RootElement("Search Hotels")
			{
				new Section()
				{
					destElement,
					startDateElement,
					endDateElement,
				},
				new Section("Room info")
				{
					roomsElement,
					adultsElement,
					childrenElement,
				},
				new Section()
				{
					searchButton,
				}
				
			};
			TableView.BackgroundView = new BackGroundView(UITheme.BackgroundImage,null,100);
			TableView.TableHeaderView = new UIImageView(UITheme.hotwireLogo);
		}
		private void Search()
		{
			destElement.FetchValue();
			if(string.IsNullOrEmpty(destElement.Value))
			{
				var alert = new UIAlertView("Error","Please enter a destination",null,"Ok");
				alert.Clicked += delegate {
					destElement.entry.BecomeFirstResponder();
				};
				alert.Show();
				return;
			}
			this.ActivateController(new HotelViewController(BuildSearchString()));
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
		
		private string BuildSearchString()
		{
			var searchString = Constants.HotelUrl + "&dest=" + destElement.Value
				+ "&startdate=" + startDateElement.DateValue.ToString("MM/dd/yyyy") 
					+ "&enddate=" + endDateElement.DateValue.ToString("MM/dd/yyyy") 
					+ "&rooms=" + roomsElement.Value
					+ "&adults=" + adultsElement.Value
					+ "&children=" + childrenElement.Value;
			return searchString;
			
		}
	}
}

