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
namespace TravelPlanner
{
	
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
					button.Click += Click;
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
	
}

