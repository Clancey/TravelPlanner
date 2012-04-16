using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
namespace TravelPlanner
{
	public class StarView : UIView
	{
		private double _starValue;
		private int OutOf;
		public static UIImage StarHalfImage = UIImage.FromFile ("Images/StarHalf.png");
		public static UIImage StarImage = UIImage.FromFile ("Images/Star.png");
		public StarView (double starValue, int outOf) : base()
		{
			OutOf = outOf;
			StarValue = starValue;
		}
		public StarView (double starValue) : this(starValue,5)
		{
			
		}
		public override void LayoutSubviews ()
		{
			var frame = this.Bounds;
			var width = frame.Width / OutOf;
			var totalWidth = width * Images.Count;
			float currentX = (frame.Width - totalWidth)/2;
			float y = (frame.Height - width)/2;
			foreach(var image in Images)
			{
				image.Frame = new System.Drawing.RectangleF(currentX,y,width,width);
				currentX += width;
			}
		}
		private List<UIImageView> Images;
		public double StarValue {
			get { return _starValue; }
			set {
				if (_starValue == value)
					return;
				_starValue = value;
				popluateImages ();
			}
		}
		private void clearImages ()
		{
			foreach (var view in this.Subviews)
				view.RemoveFromSuperview ();
		}
		private void popluateImages ()
		{
			clearImages ();
			Images = new List<UIImageView> ();
			var wholeNumber = Math.Truncate(_starValue);
			bool isHalf = (_starValue - wholeNumber) != 0;
			for(int i = 1;i <= wholeNumber;i++)
				Images.Add(new UIImageView(StarImage));
	
			if(isHalf)
				Images.Add(new UIImageView(StarHalfImage));
			this.AddSubviews(Images.ToArray());
		}
	}
}

