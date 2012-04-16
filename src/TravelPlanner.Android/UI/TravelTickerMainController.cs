
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
	[Activity (Label = "TravelTickerMainController")]			
	public partial class TravelTickerMainController : BaseActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
		}
		
		public override void CreateRoot ()
		{
			Root = new RootElement ("Hotel Search");
			PopulateRoot();
		}
		private void ShowDeals(string url, string title)
		{
			
			var intent = new Intent(this, typeof(TravelTickerController));
			intent.PutExtra("TravelTickerUrl",url);
			intent.PutExtra("Title",title);
			StartActivity(intent);
		}
	}
}

