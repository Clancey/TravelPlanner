
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

namespace TravelPlanner
{
	[Activity (Label = "Details")]			
	public partial class HotelDetailController : BaseActivity
	{
		HotelResult Deal;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Deal = (HotelResult)StateManager.GetObject("CurrentHotelResult");
			Console.WriteLine(_deal.Title);
			// Create your application here
		}

		
		public override void CreateRoot ()
		{
			Root = new RootElement ("Details"){new Section()};
			PopulateRoot();
			LoadingComplete();
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

