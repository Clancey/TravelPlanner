using System;
using MonoTouch.UIKit;
using System.Threading;
using MonoTouch.Dialog;
using ClanceysLib;
using System.Threading.Tasks;
namespace TravelPlanner
{
	public class HotelViewController : MyDialogViewController
	{
		string _url;
		MBProgressHUD _loading;
		HotelSearch.HotelSearchResults _result;
		
		public HotelViewController (string url) : base(new RootElement("Search Results"), true)
		{
			this.Root.UnevenRows = true;
			this.Style = UITableViewStyle.Plain;
			_url = url;
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationController.NavigationBar.TintColor = Theme.NavigationTint;
			
			_loading = new MBProgressHUD(UIApplication.SharedApplication.KeyWindow);
			_loading.RectangleColor = UIColor.White;
			_loading.RectangleBorderColor = UIColor.FromRGB(165,0,1);
			_loading.CustomView = new LoadingImageView();
			_loading.TitleText = "Searching...";
			_loading.TextColor = UIColor.FromRGB(165,0,1);
			_loading.Mode = MBProgressHUDMode.Custom;
			
			_loading.Show(true);
		}
		
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			
			Task.Factory.StartNew(() => {
				_result = DataAccess.FetchHotelSearchResults(_url);
				BeginInvokeOnMainThread(() => {
					ReloadUI();
				});
			});
		}
		
		private void ReloadUI()
		{
			this.NavigationItem.RightBarButtonItem = new UIBarButtonItem("Map", UIBarButtonItemStyle.Plain, delegate {
				var mapVc = new MapViewController();
				this.ActivateController(mapVc);
			});
			//Console.WriteLine("update complete");
			Section section = new Section();
			
			foreach(var deal in _result.Results)
				section.Add(new HotelResultElement(deal));	
			
			this.Root.Clear();
			this.Root.Add(section);
			this.Root.TableView.ReloadData();
			
			_loading.Hide(true);
			
		}
		
	}
}

