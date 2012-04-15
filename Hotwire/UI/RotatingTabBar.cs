// 
//  Copyright 2011  James Clancey
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
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.iAd;
using MonoTouch.Foundation;
using ClanceysLib;
namespace Hotwire
{

	public class RotatingTabBar : UITabBarController {
		UIView indicator;
		int selected;
		int DefaultCount = 5;
		private ADBannerView adView;
		public RotatingTabBar () : base ()
		{
			
		}		
		public RotatingTabBar (int numberOfTabs) : base ()
		{
			DefaultCount = numberOfTabs;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}

		void UpdatePosition (bool animate)
		{
			var count = this.ViewControllers.Length;
			if (count == 0)
				count = DefaultCount;
			var w = View.Bounds.Width/count;
			var x = w * selected;
			
			if (animate){
				UIView.BeginAnimations (null);
				UIView.SetAnimationCurve (UIViewAnimationCurve.EaseInOut);
			}
			
			indicator.Frame = new RectangleF (x+((w-10)/2), View.Bounds.Height-TabBar.Bounds.Height-4, 10, 6);
			this.View.BringSubviewToFront(indicator);
#if LITE
			if(adView != null)
				adView.Frame = adView.Frame.SetLocation(new SizeF(0,indicator.Frame.Y + indicator.Frame.Height - adView.Frame.Height));
#endif
			
			if (animate)
				UIView.CommitAnimations ();
		}
		
		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);
			
			#if LITE	
			if(this.InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || this.InterfaceOrientation == UIInterfaceOrientation.LandscapeRight)
				adView.CurrentContentSizeIdentifier = ADBannerView.SizeIdentifierLandscape;
			else 
				adView.CurrentContentSizeIdentifier = ADBannerView.SizeIdentifierPortrait;	
			
			//TableView.Frame = orgTableFrame.Subtract(new SizeF(0,adView.Frame.Height));
			//adView.Frame = adView.Frame.SetLocation(new SizeF(0,TableView.Frame.Height));
			//this.View.Superview.AddSubview(adView);
			#endif
			
			UpdatePosition (false);
			if(AdChanged != null)
				AdChanged(this,null);
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		
			#if LITE
			if(adView == null)
			{
				adView = new ADBannerView();				
				NSMutableSet nsM = new NSMutableSet();
				nsM.Add(ADBannerView.SizeIdentifierLandscape);
				nsM.Add(ADBannerView.SizeIdentifierPortrait);
				adView.RequiredContentSizeIdentifiers = nsM;
				adView.AdLoaded += delegate {
					adView.Hidden = false;
					UpdatePosition(false);
					if(AdChanged != null)
						AdChanged(this,null);
					//adView.Frame = adView.Frame.SetLocation(new SizeF(0,TableView.Frame.Height));
				};
				adView.FailedToReceiveAd += delegate(object sender, AdErrorEventArgs e) {
					Console.WriteLine(e.Error);
					adView.Hidden = true;
					if(AdChanged != null)
						AdChanged(this,null);
					//adView.Frame = adView.Frame.SetLocation(new SizeF(0,TableView.Frame.Height));
				};
			}
			
			if(this.InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || this.InterfaceOrientation == UIInterfaceOrientation.LandscapeRight)
				adView.CurrentContentSizeIdentifier = ADBannerView.SizeIdentifierLandscape;
			else 
				adView.CurrentContentSizeIdentifier = ADBannerView.SizeIdentifierPortrait;
					
			//adView.Frame = adView.Frame.SetLocation(new SizeF(0,indicator.Frame.Y + indicator.Frame.Height - adView.Frame.Height));
			this.View.AddSubview(adView);
			//adView.Hidden = false;
			#endif
			
			if (indicator == null){
				indicator = new TriangleView (UIColor.FromRGB (0.26f, 0.26f, 0.26f), UIColor.Black);
				View.AddSubview (indicator);
				ViewControllerSelected += OnSelected;
				UpdatePosition (false);
			}
		}
		
		public void OnSelected (object sender, UITabBarSelectionEventArgs a)
		{
			var vc = ViewControllers;
			
			for (int i = 0; i < vc.Length; i++){
				if (vc [i] == a.ViewController){
					selected = i;
					UpdatePosition (true);
					return;
				}
			}
		}
		
		public float Adheight {
			get {
				if(adView == null || adView.Hidden)
					return 0;
				return adView.Bounds.Height;
			}
		}
		public event EventHandler AdChanged;
	}
	
	
	public class TriangleView : UIView {
		UIColor fill, stroke;
		
		public TriangleView (UIColor fill, UIColor stroke) 
		{
			Opaque = false;
			this.fill = fill;
			this.stroke = stroke;
		}
		
		public override void Draw (RectangleF rect)
		{
			var context = UIGraphics.GetCurrentContext ();
			var b = Bounds;
			
			fill.SetColor ();
			context.MoveTo (0, b.Height);
			context.AddLineToPoint (b.Width/2, 0);
			context.AddLineToPoint (b.Width, b.Height);
			context.ClosePath ();
			context.FillPath ();
			
			stroke.SetColor ();
			context.MoveTo (0, b.Width/2);
			context.AddLineToPoint (b.Width/2, 0);
			context.AddLineToPoint (b.Width, b.Width/2);
			context.StrokePath ();
		}
	}
}

