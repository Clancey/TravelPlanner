using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using System.Threading;
using TravelPlanner.TravelTicker;
using ClanceysLib;
namespace TravelPlanner
{
	public class TravelTickerViewController : MyDialogViewController
	{
		TravelTickerSearchResults result;
		MBProgressHUD loading; 
		string Url;
		public TravelTickerViewController (string url,string title,bool pushing) : base(null,pushing)
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
			result = DataAccess.FetchTravelTickerResults(Url);
			this.InvokeOnMainThread(delegate{
				ReloadUI();
			});
		}
		private void ReloadUI()
		{
			Console.WriteLine("update complete");
			Section section = new Section();
			foreach(var deal in result.Result)
			{
				Console.WriteLine("adding deal");
				section.Add(new TravelTickerDealElement(deal));	
			}
			Console.WriteLine("creating root");
			Root = new RootElement("Travel-Ticker"){section};
			loading.Hide(true);
			Console.WriteLine("reload complete");
		}
	}
}

