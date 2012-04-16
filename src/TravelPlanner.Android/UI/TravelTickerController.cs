
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
	[Activity (Label = "TravelTickerController")]			
	public partial class TravelTickerController : BaseActivity
	{
		string title;
		protected override void OnCreate (Bundle bundle)
		{
			Url = Intent.GetStringExtra("TravelTickerUrl");
			title = Intent.GetStringExtra("Title");
			base.OnCreate (bundle);

		}
		
		public override void CreateRoot ()
		{
			Root = new RootElement (title){new Section()};
			GetData();
		}

		private void GetDataComplete()
		{
			this.RunOnUiThread(delegate{
				PopulateRoot();
			});
		}
	}
}

