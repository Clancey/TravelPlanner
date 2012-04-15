using System;
using MonoTouch.UIKit;
using System.Threading;
using MonoTouch.Dialog;
using ClanceysLib;
namespace TravelPlaner
{
	public class HotelViewController : MyDialogViewController
	{
		MBProgressHUD loading; 
		string Url;
		HotelSearch.HotelSearchResults result;
		public HotelViewController (string url) : base (null,true)
		{
			this.Style = UITableViewStyle.Plain;
			this.Title = "Search Results";
			Url = url;	
		}
			
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear(true);
			if(loading == null)
			{
				loading = new MBProgressHUD(UIApplication.SharedApplication.KeyWindow);
				loading.RectangleColor = UIColor.White;
				loading.RectangleBorderColor = UIColor.FromRGB(165,0,1);
				loading.CustomView = new LoadingImageView();
				loading.TitleText = "Searching...";
				loading.TextColor = UIColor.FromRGB(165,0,1);
			
				loading.Mode = MBProgressHUDMode.Custom;
			}
			
			this.NavigationController.NavigationBar.TintColor = UITheme.NavigationTint;
			if(result == null)
				Refresh();
		}
		private void Refresh()
		{
			loading.Show(true);
			Thread thread = new Thread(update);	
			thread.Start();
		}
		
		private void update()
		{
			result = DataAccess.FetchHotelSearchResults(Url);
			this.InvokeOnMainThread(delegate{
				ReloadUI();
			});
		}
		private void ReloadUI()
		{
			
			this.NavigationItem.RightBarButtonItem = new UIBarButtonItem("Map",UIBarButtonItemStyle.Plain,delegate {
				var mapVc = new MapViewController();
				this.ActivateController(mapVc);
			});
			//Console.WriteLine("update complete");
			Section section = new Section();
			
			foreach(var deal in result.Results)
				section.Add(new HotelResultElement(deal));	
			
			
			//Console.WriteLine("creating root");
			Root = new RootElement("Search Results"){section};
			loading.Hide(true);
			//Console.WriteLine("reload complete");
		}
		
	}
}

