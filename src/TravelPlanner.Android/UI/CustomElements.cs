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
using System.IO;
using System.ComponentModel;
using TravelPlanner.HotelSearch;


namespace TravelPlanner
{
	
	public delegate void DateSelected (DateTime date);
	public class ButtonElement : StringElement
	{
		public enum ButtonColor
		{
			Blue,
			Red,
			Yellow,
			Black,
			Gray,
		}
		private ButtonColor _color;
		private int _textColor;

		public ButtonElement (string caption,ButtonColor color) : this(caption,color,null)
		{

		}
		public ButtonElement (string caption,ButtonColor color, Action tapped) : this (caption,color,tapped, Android.Graphics.Color.White)
		{

		}

		public ButtonElement (string caption, ButtonColor color,Action tapped,  int textColor)
			: base(caption, (int)DroidResources.ElementLayout.dialog_button)
		{
			if(tapped != null)
				this.Click += delegate{tapped();};
			_color = color;
			_textColor = textColor;
		}

		public override View GetView (Context context, View convertView, ViewGroup parent)
		{
			Button button;
			var view = DroidResources.LoadButtonLayout (context, convertView, parent, LayoutId, out button);
			if (view != null) {
				button.Text = Caption;
				if (Click != null)
					button.Click += delegate {
						Click();
					};
			}
			button.SetTextColor (_textColor);
			switch (_color) {
			case ButtonColor.Blue:
				button.SetBackgroundResource (Resource.Drawable.btn_blue);
				break;
			case ButtonColor.Red:
				button.SetBackgroundResource (Resource.Drawable.btn_red);
				break;
			case ButtonColor.Yellow:
				button.SetBackgroundResource (Resource.Drawable.btn_yellow);
				break;
			case ButtonColor.Black:
				button.SetBackgroundResource (Resource.Drawable.btn_black);
				break;
			default:
				button.SetBackgroundResource (Resource.Drawable.btn_gray);
				break;
			}
			return view;
		}

		public override string Summary ()
		{
			return Caption;
		}
	}

	public class CalendarElement : StringElement
	{
		public bool closeOnSelect;
		DatePickerDialog _dateDialog;
		public EventHandler<DatePickerDialog.DateSetEventArgs> OnDateSelected;
		
		public DateTime DateValue { get; set; }
		
		public CalendarElement(string caption, DateTime dateValue) : base (caption)
		{
			this.DateValue = dateValue;			
		}
		
		public override View GetView (Context context, View convertView, ViewGroup parent)
		{
			if(_dateDialog == null) {
				_dateDialog = new DatePickerDialog(context, OnDateSet, DateValue.Year, DateValue.Month, DateValue.Day);
			}
			
			return base.GetView (context, convertView, parent);
		}

		public override void Selected ()
		{
			base.Selected ();
			
			var activity = this.GetContext() as Activity;
			
			if(activity != null && _dateDialog != null) {
				_dateDialog.Show();
			}
			else {
				Console.WriteLine("Invalid Activity");
			}
		}
		
        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            this.DateValue = e.Date;
			
			var activity = this.GetContext() as Activity;
			
			activity.RunOnUiThread(() => {
				this.Value = string.Format("{0:MM/dd/yyyy}", DateValue.Date);
			});
        }
	}

	public class ComboBoxElement : StringElement
	{
		AlertDialog _alertDialog;
		string _caption = "";
		string[] _itemArray;
		
		public ComboBoxElement(string caption, object[] items, string displayMember) : base(caption)
		{
			if(items == null) {
				throw new ArgumentException("Invalid parameter 'items'.");
			}
			
			if(items.Any() == false) {
				throw new ArgumentException("Invalid parameter 'items'.");
			}
			
			
			
			_caption = caption;
			
			if(string.IsNullOrWhiteSpace(displayMember)) {
				_itemArray = items.Select(x=> x.ToString()).ToArray();
			}
			else {
				_itemArray = items.Select(x => Util.GetPropertyValue(x, displayMember)).ToArray();
			}
			_itemArray.ToString();
		}
		
		public override View GetView (Context context, View convertView, ViewGroup parent)
		{
			if(_itemArray.Any()) {
				BuildDialog();
			}
			
			return base.GetView (context, convertView, parent);
		}
		
		public override void Selected ()
		{
			base.Selected ();
			_alertDialog.Show();
			
		}
		
		private void BuildDialog()
		{
			if(_alertDialog != null) {
				return;
			}
			
			
			
			var builder = new AlertDialog.Builder(this.GetContext());
			builder.SetTitle(_caption);
			

			builder.SetItems(_itemArray, (o, e) => {
				
				Console.WriteLine("Selected: {0}", e.Which);
				this.Value = _itemArray[e.Which];
			});
			
			_alertDialog = builder.Create();
		}
	}

	public class HotelResultElement : StringElement
	{
		public HotelResultElement(TravelPlanner.HotelSearch.HotelResult result,bool include,Action tapped) : base(result.Title,result.AveragePricePerNight.ToString("C2"))
		{

		}
	}
	public class HotelAmenityElement : StringElement
	{
		TravelPlanner.HotelSearch.Amenity Amenity;
		public HotelAmenityElement(TravelPlanner.HotelSearch.Amenity amenity) : base (amenity.Name)
		{
			Amenity = amenity;
		}
	}
	public class HotelHeaderElement : StringElement
	{
		public HotelHeaderElement (HotelResult deal) : base ("")
		{
		}
	}
	public class TravelTickerDealElement : StringElement
	{
		TravelPlanner.TravelTicker.TravelTickerDeal Deal;
		public TravelTickerDealElement (TravelPlanner.TravelTicker.TravelTickerDeal deal) : base (deal.Title)
		{
			Deal = deal;
		}
	}
}

