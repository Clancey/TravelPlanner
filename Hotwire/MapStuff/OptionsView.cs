using System;
using MonoTouch.UIKit;
using ClanceysLib;
using System.Drawing;
namespace ParkApp
{
	public class OptionsView : UIView
	{
		UIGlassyButton AttractionsBtn;
		UIGlassyButton RestaurantsBtn;
		UIGlassyButton RestroomsBtn;
		
		public int LayerSelection;
		public Action Clicked;
		
		public OptionsView ()
		{
			this.Frame = UIApplication.SharedApplication.KeyWindow.Bounds;
			this.Add(new UIView(this.Frame){BackgroundColor = UIColor.DarkGray, Alpha = .5f});
		
			this.BackgroundColor = UIColor.Clear;
			thePanel = new StackPanel(new RectangleF(50,0,200,200));
			thePanel.BackgroundColor = UIColor.White;
			thePanel.StretchWidth = true;
			
			AttractionsBtn = new UIGlassyButton(new RectangleF(0,0,100,25)){Title = "Attractions",Color = UIColor.Purple};
			AttractionsBtn.TouchDown += delegate {
				LayerSelection = 0;
				this.RemoveFromSuperview();
				if(Clicked != null)
					Clicked();
				
			};
			
			RestaurantsBtn = new UIGlassyButton(new RectangleF(0,0,100,25)){Title = "Attractions",Color = UIColor.Yellow,};
			RestaurantsBtn.TouchDown += delegate {
				LayerSelection = 1;
				this.RemoveFromSuperview();
				if(Clicked != null)
					Clicked();
				
			};
			RestaurantsBtn.SetTitleColor(UIColor.Black,UIControlState.Normal);
			
			RestroomsBtn = new UIGlassyButton(new RectangleF(0,0,100,25)){Title = "Attractions",Color = UIColor.Blue};
			RestroomsBtn.TouchDown += delegate {
				LayerSelection = 2;
				this.RemoveFromSuperview();
				if(Clicked != null)
					Clicked();
				
			};
			
			thePanel.Add(AttractionsBtn);
			thePanel.Add(RestaurantsBtn);
			thePanel.Add(RestroomsBtn);
			this.Add(thePanel);
			
			
		}
		
		public StackPanel thePanel;
	}
}

