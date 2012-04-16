using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using System.Threading;
using TravelPlanner.TravelTicker;
using ClanceysLib;
namespace TravelPlanner
{
	public partial class TravelTickerController : MyDialogViewController
	{
		MBProgressHUD loading; 
		public TravelTickerController (string url,string title,bool pushing) : base(new RootElement("Travel-Ticker"),pushing)
		{
			Url = url;
			this.Style = UITableViewStyle.Plain;
			this.Title = title;
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
			
			if(result == null)
			{
				loading.Show(true);
				GetData();
			}
		}

		private void GetDataComplete()
		{
		this.InvokeOnMainThread(delegate{
				PopulateRoot();
				loading.Hide(true);
			});
		}

	}
}

