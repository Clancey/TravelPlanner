
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

namespace TravelPlanner
{
	[Activity (Label = "HotelViewController")]			
	public partial class HotelViewController : BaseActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			_url = Intent.GetStringExtra("SearchUrl");
		}

		
		public override void CreateRoot ()
		{
			Root = new RootElement ("Search Results"){new Section()};
			GetData();
		}
		private void GetDataComplete()
		{
			PopulateRoot();
		}
		private void LoadingComplete()
		{

		}
	}
}
