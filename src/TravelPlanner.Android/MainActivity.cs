
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

namespace TravelPlanner
{

	[Activity (Label = "Travel Planner", Theme="@android:style/Theme.NoTitleBar", MainLauncher = true )]			
	public class MainActivity : TabActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			SetContentView (Resource.Layout.TabLayout);
			TabHost.TabSpec spec;
			var intent = new Intent(this,typeof(HotelSearchController));
			spec = TabHost.NewTabSpec("hotels");
			spec.SetContent(intent);
			spec.SetIndicator("Hotels");
			TabHost.AddTab(spec);

			intent = new Intent(this,typeof(CarSearchController));
			spec = TabHost.NewTabSpec("cars");
			spec.SetContent(intent);
			spec.SetIndicator("Cars");
			TabHost.AddTab(spec);
			
			intent = new Intent(this,typeof(TravelTickerMainController));
			spec = TabHost.NewTabSpec("travel");
			spec.SetContent(intent);
			spec.SetIndicator("Travel-Ticker");
			TabHost.AddTab(spec);



			// Create your application here
		}
	}
}

