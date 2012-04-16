// 
//  Copyright 2012  Xamarin Inc  (http://www.xamarin.com)
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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
using Android.Graphics;
using Android.Content.PM;

namespace TravelPlanner
{
	
	public abstract class BaseActivity : Activity
	{
		public LinearLayout LayoutControl;
		public Action<int, Result, Intent> ResultReturned {get;set;}
		public RootElement Root;
		DialogView dv;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//Settings.Setup(this);
			SetContentView (Resource.Layout.BaseActivity);			
			LayoutControl = FindViewById<LinearLayout> (Resource.Id.linerLayout);
			CreateRoot();
			var dv = new DialogView(this,Root);
			LayoutControl.AddView (dv);
		}
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if(ResultReturned != null)
				ResultReturned(requestCode, resultCode, data);
			
			//base.OnActivityResult (requestCode, resultCode, data);
		}
		public abstract void CreateRoot();
	}
}

