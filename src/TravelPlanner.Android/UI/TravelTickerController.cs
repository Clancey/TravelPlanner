
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
	public class TravelTickerController : BaseActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

		}
		
		public override void CreateRoot ()
		{
			Root = new RootElement ("Travel-Ticker"){new Section()};
			//PopulateRoot();
		}
	}
}

