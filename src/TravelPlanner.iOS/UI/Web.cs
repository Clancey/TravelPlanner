//
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
//
//
using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using System.Drawing;
using Mono.Util;

namespace TravelPlanner
{
	public class WebViewController : UIViewController {
		UIToolbar toolbar;
		UIBarButtonItem backButton, forwardButton, stopButton, refreshButton;
		protected UIWebView WebView;

		public WebViewController ()
		{
			var fixedSpace = new UIBarButtonItem (UIBarButtonSystemItem.FixedSpace, null) {
				Width = 26
			};
			var flexibleSpace = new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace, null);

			toolbar = new UIToolbar (){TintColor = UITheme.NavigationTint};

			
			//this.NavigationItem.RightBarButtonItem = new UIBarButtonItem (Locale.GetText ("Close"), UIBarButtonItemStyle.Bordered, (o, e) => {  this.NavigationController.PopViewControllerAnimated (true);} );
			
			backButton = new UIBarButtonItem (UIImage.FromBundle ("Images/back.png"), UIBarButtonItemStyle.Plain, (o, e) => { WebView.GoBack (); });
			forwardButton = new UIBarButtonItem (UIImage.FromBundle ("Images/forward.png"), UIBarButtonItemStyle.Plain, (o, e) => { WebView.GoForward (); });
			refreshButton = new UIBarButtonItem (UIBarButtonSystemItem.Refresh, (o, e) => { WebView.Reload(); });
			stopButton = new UIBarButtonItem (UIBarButtonSystemItem.Stop, (o, e) => { WebView.StopLoading (); });
			
			toolbar.Items = new UIBarButtonItem [] { backButton, fixedSpace, forwardButton, flexibleSpace, stopButton, fixedSpace, refreshButton };

			//View.AddSubview (topBar);
			View.AddSubview (toolbar);
						
		}

		void UpdateNavButtons ()
		{
			if (WebView == null)
				return;
			
			backButton.Enabled = WebView.CanGoBack;
			forwardButton.Enabled = WebView.CanGoForward;
		}
		
		protected virtual string UpdateTitle ()
		{
			return WebView.EvaluateJavascript ("document.title");			
		}
		
		public void SetupWeb (string initialTitle)
		{
			WebView = new UIWebView (){
				ScalesPageToFit = true,
				MultipleTouchEnabled = true,
				AutoresizingMask = UIViewAutoresizing.FlexibleHeight|UIViewAutoresizing.FlexibleWidth,
			};
			WebView.LoadStarted += delegate { 
				stopButton.Enabled = true;
				refreshButton.Enabled = false;
				UpdateNavButtons ();
				
				ClanceysLib.Util.PushNetworkActive (); 
			};
			WebView.LoadFinished += delegate {
				stopButton.Enabled = false;
				refreshButton.Enabled = true;
				//Util.PopNetworkActive (); 
				UpdateNavButtons ();
				
				Title = UpdateTitle ();
			};
			
			Title = initialTitle;
			View.AddSubview (WebView);
			backButton.Enabled = false;
			forwardButton.Enabled = false;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown;
		}
		
		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			/*
			if (WebView != null){
				if(WebView.IsLoading)
					Util.PopNetworkActive();
				WebView.StopLoading();
			}
			*/		
				
		}
		void LayoutViews ()
		{
			var sbounds = View.Bounds;
			int top = (InterfaceOrientation == UIInterfaceOrientation.Portrait) ? 0 : -44;
			
			toolbar.Frame = new RectangleF (0, top, sbounds.Width, 44);
			//toolbar.Frame =  new RectangleF (0, sbounds.Height-44, sbounds.Width, 44);
			WebView.Frame = new RectangleF (0, top+44, sbounds.Width, sbounds.Height-44-top);
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			LayoutViews ();
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);
			LayoutViews ();
		}
		
		public static string EncodeIdna (string host)
		{
			foreach (char c in host){
				if (c > (char) 0x7f){
					var segments = host.Split ('.');
					var encoded = new string [segments.Length];
					int i = 0;
					
					foreach (var s in segments)
						encoded [i++] = Punycode.Encode (s.ToLower ());
					
					
					return "xn--" + string.Join (".", encoded);
				}
			}
			return host;
		}
		
		public void OpenUrl (DialogViewController parent, string url,string title)
		{
			UIView.BeginAnimations ("foo");
			HidesBottomBarWhenPushed = false;
			SetupWeb (title);
			
			if (url.StartsWith ("http://")){
				string host;
				int last = url.IndexOf ('/', 7);
				if (last == -1)
					host = url.Substring (7);
				else 
					host = url.Substring (7, last-7);
				url = "http://" + EncodeIdna (host) + (last == -1 ? "" : url.Substring (last));
				url = url.Replace(" ","%20");
			}
			//url = "http://www.travel-ticker.com/Destination/New%20York?startDate=mm%2Fdd%2Fyy&encId=b9svopln39q6q3y25c52&vert=H&endDate=mm%2Fdd%2Fyy&destination=JFK%2CJFK";
			//url = "http://www.travel-ticker.com/category.jsp?actionType=1&encId=b9svopln39q6q3y25c52&sid=S298&bid=B311402&categoryType=Destination&categoryName=New%20York&startDate=mm%2Fdd%2Fyy&endDate=mm%2Fdd%2Fyy&vert=hotel&dest=JFK,JFK";
			
			var nsUrl = new NSUrl (url);
			WebView.LoadRequest (new NSUrlRequest(nsUrl));
			
			parent.ActivateController (this);
			UIView.CommitAnimations ();
		}
		
		public void OpenHtmlString (DialogViewController parent, string htmlString, NSUrl baseUrl)
		{
			UIView.BeginAnimations ("foo");
			HidesBottomBarWhenPushed = true;
			SetupWeb ("");
			
			WebView.LoadHtmlString (htmlString, baseUrl);
			parent.ActivateController (this);
			UIView.CommitAnimations ();
		}
		

	}
}

