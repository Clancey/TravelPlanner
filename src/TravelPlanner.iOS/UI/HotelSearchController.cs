using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
using ClanceysLib;
namespace TravelPlanner
{
	public partial class HotelSearchController : MyDialogViewController
	{
		public HotelSearchController () : base(new RootElement("Search Hotels"),false)
		{
			
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			PopulateRoot();
			TableView.BackgroundView = new BackGroundView(Theme.BackgroundImage,null,100);
			
		}
		private void Search()
		{
			destElement.BecomeFirstResponder(false);
			destElement.ResignFirstResponder(false);
			
			destElement.FetchValue();
			if(string.IsNullOrEmpty(destElement.Value))
			{
				var alert = new UIAlertView("Error","Please enter a destination",null,"Ok");
				alert.Clicked += delegate {
					destElement.BecomeFirstResponder(true);
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

