// 
//  Copyright 2012  James Clancey, Xamarin Inc  (http://www.xamarin.com)
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
using System.Drawing;
using TravelPlanner.TravelTicker;
using System.Threading.Tasks;
#if MONOTOUCH
using MonoTouch.Dialog;
using ClanceysLib;
#elif MONODROID
using MonoDroid.Dialog;
#endif
using System.Globalization;
namespace TravelPlanner
{
	public partial class TravelTickerController
	{
		
		string Url;
		TravelTickerSearchResults result;
		private void GetData ()
		{
			Task.Factory.StartNew (() => {
				result = DataAccess.FetchTravelTickerResults (Url);
				GetDataComplete ();
			});
		}
		
		private void PopulateRoot ()
		{
			Section section = new Section();
			if(result == null)
				return;
			foreach(var deal in result.Result)
			{
				section.Add(new TravelTickerDealElement(deal));	
			}

			this.Root.Clear();
			this.Root.Add(section);
			this.ReloadData();
		}
	}
}

