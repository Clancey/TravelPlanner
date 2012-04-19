
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
	[Activity (Label = "HotelViewController")]			
	public partial class HotelViewController : BaseActivity
	{
		ProgressDialog _progressDialog;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			_url = Intent.GetStringExtra("SearchUrl");
			
			_progressDialog = new ProgressDialog(this);
			_progressDialog.SetTitle("Loading");
			_progressDialog.Show();
		}

		
		public override void CreateRoot ()
		{
			Root = new RootElement ("Search Results"){new Section()};
			GetData();
		}
		private void GetDataComplete()
		{
			this.RunOnUiThread(delegate{
				PopulateRoot();
			});
		}
		private void LoadingComplete()
		{
			this.ReloadData();
			_progressDialog.Dismiss();
		}
		
		private void ResultClicked(HotelResult deal)
		{
			StateManager.SetObject("CurrentHotelResult",deal);
			var intent = new Intent(this, typeof(HotelDetailController));
			StartActivity(intent);
		}
	}
}

