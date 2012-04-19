
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MonoDroid.Dialog;
using TravelPlanner.HotelSearch;
using System.Threading.Tasks;

namespace TravelPlanner
{
	[Activity (Label = "Details")]			
	public partial class HotelDetailController : BaseActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			
		}
		
		
		public override void CreateRoot ()
		{
			_deal = StateManager.GetObject("CurrentHotelResult") as HotelResult;
			Console.WriteLine(_deal.Title);
			
			Root = new RootElement ("Details") { new Section() };
			
			Task.Factory.StartNew(() => {
				PopulateRoot();
				LoadingComplete();
			});
			
		}
		private void purchase()
		{
			Intent intent = new Intent(Intent.ActionView, 
			Android.Net.Uri.Parse(_deal.Url));
			StartActivity(intent);	
		}
		
		private void showMap()
		{

		}
		
		private void LoadingComplete()
		{
			RunOnUiThread(() => {
				this.ReloadData();
			});
		}
	}
}

