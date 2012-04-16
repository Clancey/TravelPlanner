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
using System.Threading.Tasks;
#if MONOTOUCH
using MonoTouch.Dialog;
using ClanceysLib;
#elif MONODROID
using MonoDroid.Dialog;
#endif
namespace TravelPlanner
{
	public partial class HotelViewController
	{
		string _url;
		HotelSearch.HotelSearchResults _result;
		private void GetData()
		{
			Task.Factory.StartNew(() => {
				_result = DataAccess.FetchHotelSearchResults(_url);
				GetDataComplete();
			});
		}
		
		private void PopulateRoot()
		{
			//Console.WriteLine("update complete");
			Section section = new Section();

			if(_result != null)
			foreach(var deal in _result.Results)
			{
				var theDeal = deal;
				var element = new HotelResultElement(deal,true,delegate{
					ResultClicked(theDeal);
				});
				section.Add(element);	
			}
			
			this.Root.Clear();
			this.Root.Add(section);
			LoadingComplete();
		}
	}
}

