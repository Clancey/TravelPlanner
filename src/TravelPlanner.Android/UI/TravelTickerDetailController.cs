
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
	[Activity (Label = "Details")]			
	public class TravelTickerDetailController : BaseActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
		}

		
		public override void CreateRoot ()
		{
			Root = new RootElement ("Details");
			//PopulateRoot();
		}
	}
}

