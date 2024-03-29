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
namespace TravelPlanner
{
	class DialogView : ListView
	{
		public DialogView(Activity context,RootElement root) : base(context)
		{
			LayoutParameters = new LayoutParams(LayoutParams.FillParent,LayoutParams.FillParent);
			/*
			LayoutParameters.Width = LayoutParams.FillParent;
			LayoutParameters.Height = LayoutParams.FillParent;
			*/
			Root = root;
		}
		
		private RootElement root;
		public RootElement Root {get{return root;}set{root = value;SetRoot();}}
		private DialogHelper dialogHelper {get;set;}
		
		private void SetRoot()
		{
			this.CacheColorHint = 0;
			dialogHelper = new DialogHelper(Context,this,root);
		}
		public void ReloadData()
		{
			dialogHelper.ReloadData();
		}
	}
}