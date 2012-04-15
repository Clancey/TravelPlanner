using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;
namespace TravelPlaner
{
	public class LoadingImageView : UIImageView
	{
		public const int StartFrame = 1;
		public const int EndFrame = 112;
		public const string FileName = "Images/loading/";
		public const string FileExtension = ".png";
		public LoadingImageView () : base (new RectangleF(0,0,66,65))
		{
			List<UIImage> images = new List<UIImage>();
			for(int i = StartFrame; i <= EndFrame; i++)
			{
				images.Add(UIImage.FromFile(FileName + i + FileExtension));	
			}
			this.AnimationImages = images.ToArray();
			this.AnimationDuration = 8;
			this.AnimationRepeatCount = 0;
			this.StartAnimating();
		}
	}
}

