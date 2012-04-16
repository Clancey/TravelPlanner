// Copyright 2010 Miguel de Icaza
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Drawing;
using MonoTouch.Dialog;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog.Utilities;

namespace TravelPlanner
{
	public class ShortProfileView : UIView, IImageUpdated
	{
		const int userSize = 19;
		const int followerSize = 13;
		const int locationSize = 14;
		const int urlSize = 14;
		const int TextX = 95;
		
		static UIFont userFont = UIFont.BoldSystemFontOfSize (userSize);
		static UIFont followerFont = UIFont.SystemFontOfSize (followerSize);
		static UIFont locationFont = UIFont.SystemFontOfSize (locationSize);
		static UIFont urlFont = UIFont.BoldSystemFontOfSize (urlSize);
		static CGPath borderPath = Graphics.MakeRoundedPath (75);
		
		UIImageView profilePic;
		Uri ImageUri;
		string Title;
		public ShortProfileView (RectangleF rect, string imageUrl, string title) : this (rect, false)
		{				
			if(string.IsNullOrEmpty(imageUrl))
				profilePic.Image = Theme.car;
			else
				this.ImageUri = new Uri(imageUrl);
			Title = title;
			UIImage img = null;
			if (ImageUri != null)
				img = ImageLoader.DefaultRequestImage (ImageUri, this);
			else if (profilePic.Image != null)
				img = profilePic.Image;
			profilePic.Image = img;
			
			
			//url.AddTarget (delegate { if (UrlTapped != null) UrlTapped (); }, UIControlEvent.TouchUpInside);
			//url.SetTitle ("http://www.gravatar.com", UIControlState.Normal);
			//url.SetTitle (user.Url, UIControlState.Highlighted);
			SetNeedsDisplay ();
		}
		
		
		
		public ShortProfileView (RectangleF rect, bool discloseButton) : base (rect)
		{
			BackgroundColor = UIColor.Clear;

			// Pics are 73x73, but we will add a border.
			profilePic = new UIImageView (new RectangleF (10, 10, 73, 73));
			profilePic.BackgroundColor = UIColor.Clear;
			AddSubview (profilePic);
			/*
			url = UIButton.FromType (UIButtonType.Custom);
			url.Font = urlFont;
			url.Font = urlFont;
			url.LineBreakMode = UILineBreakMode.TailTruncation;
			url.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			url.TitleShadowOffset = new SizeF (0, 1);
			url.SetTitleColor (UIColor.FromRGB (0x32, 0x4f, 0x85), UIControlState.Normal);
			url.SetTitleColor (UIColor.Red, UIControlState.Highlighted);
			url.SetTitleShadowColor (UIColor.White, UIControlState.Normal);
			url.Frame = new RectangleF (TextX, 70, rect.Width-TextX, urlSize);
			
			AddSubview (url);
			 */
			if (discloseButton){
				var button = UIButton.FromType (UIButtonType.DetailDisclosure);
				button.Frame = new RectangleF (290, 36, 20, 20);
				AddSubview (button);
				//button.TouchDown += delegate { Tapped (); };
			}
		}
		// Used to update asynchronously our display when we get enough information about the tweet detail

		
		public override void Draw (RectangleF rect)
		{

			var w = rect.Width-TextX;
			var context = UIGraphics.GetCurrentContext ();
			
			context.SaveState ();
			context.SetRGBFillColor (0, 0, 0, 1);
			context.SetShadowWithColor (new SizeF (0, -1), 1, UIColor.White.CGColor);
			
			DrawString (Title, new RectangleF (TextX, 12, w, 75), userFont, UILineBreakMode.TailTruncation);
			//DrawString (user.Location, new RectangleF (TextX, 50, w, locationSize), locationFont, UILineBreakMode.TailTruncation);
			
			UIColor.DarkGray.SetColor ();
			//DrawString (user.FollowersCount + " followers", new RectangleF (TextX, 34, w, followerSize), followerFont);

			//url.Draw (rect);
			
			// Spicy border around the picture
			context.RestoreState ();
			
			context.TranslateCTM (9, 9);
			context.AddPath (borderPath);
			context.SetRGBStrokeColor (0.5f, 0.5f, 0.5f, 1);
			context.SetLineWidth (0.5f);
			context.StrokePath ();
		}

		#region IImageUpdated implementation
		void IImageUpdated.UpdatedImage (Uri uri)
		{
			profilePic.Image = ImageLoader.DefaultRequestImage (ImageUri, this);
		}
		#endregion
	}
}
