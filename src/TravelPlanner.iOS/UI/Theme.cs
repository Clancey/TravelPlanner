using System;
using MonoTouch.UIKit;
using System.Drawing;
using ClanceysLib;
namespace TravelPlanner
{
	public static class Theme
	{
		public static UIColor NavigationTint = UIColor.FromRGB(43,49,53);//(0.447f, 0.5f, 0.553f);
		public static UIColor IconColor = UIColor.FromRGB(156,0,0);//Green;//FromRGB(178,205,102);//
		public static UIImage BackgroundImage = UIImage.FromFile("Images/paper.png");
		public static UIColor BackgroundColor = UIColor.FromPatternImage(BackgroundImage);
		
		//public static UIImage FriendsImage = UIImage.FromFile("Images/friends.png");
		//public static UIImage ShoppingImage = UIImage.FromFile("Images/bigShoppingCart.png");
		public static UIImage car = UIImage.FromFile("Images/car_icon.jpg");
		public static UIImage hotwireLogo = UIImage.FromFile("Images/hotwireLogo.png");

	}
	
	public class BackGroundView : UIView
	{
		UIImageView BgImage;
		UIImageView CenteredImage;
		RectangleF centeredRect;
		int ImageH;
		
		public BackGroundView(UIImage background, UIImage centeredImage,int imageH)
		{
			BgImage = new UIImageView(background);
			if(centeredImage != null)
			{
				CenteredImage = new UIImageView(centeredImage);
				centeredRect = CenteredImage.Frame;
			}
			ImageH = imageH;
			this.AddSubview(BgImage);
			if(centeredImage != null)
				this.AddSubview(CenteredImage);
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			BgImage.Frame = BgImage.Frame.SetWidth(Frame.Width);
			var x = (Frame.Width - centeredRect.Width)/2;
			if(CenteredImage != null)
				CenteredImage.Frame = CenteredImage.Frame.SetLocation(new PointF(x,ImageH));
				
			
			
		}
	}
}

