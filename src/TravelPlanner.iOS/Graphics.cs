//
// Utilities for dealing with graphics
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
using System;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

namespace TravelPlanner
{
	public static class Graphics
	{
		static CGPath smallPath = MakeRoundedPath (48);
		static CGPath largePath = MakeRoundedPath (73);
		
		public static UIImage AdjustImage(UIImage template, CGBlendMode mode,float red,float green,float blue,float alpha )
		{
			return AdjustImage(new RectangleF(PointF.Empty,template.Size),template,mode,red,green,blue,alpha);
		}
		
		public static UIImage AdjustImage(RectangleF rect,UIImage template, CGBlendMode mode,UIColor color)
		{
			if(color == null)
				return template;
			float red = new float();
			float green = new float();
			float blue = new float();
			float alpha = new float();
			if (color == null)
				color = UIColor.FromRGB(100,0,0);
			color.GetRGBA(out red,out green, out blue, out alpha);
			return 	AdjustImage(rect,template,mode,red,green,blue,alpha);
		}
		
		public static UIImage AdjustImage(UIImage template, CGBlendMode mode,UIColor color)
		{
			float red = new float();
			float green = new float();
			float blue = new float();
			float alpha = new float();
			if (color == null)
				color = UIColor.FromRGB(100,0,0);
			color.GetRGBA(out red,out green, out blue, out alpha);
			return 	AdjustImage(new RectangleF(PointF.Empty,template.Size),template,mode,red,green,blue,alpha);
		}
		
		public static UIImage AdjustImage(RectangleF rect,UIImage template, CGBlendMode mode,float red,float green,float blue,float alpha )
		{
			using (var cs = CGColorSpace.CreateDeviceRGB ()){
				using (var context = new CGBitmapContext (IntPtr.Zero, (int)rect.Width, (int)rect.Height, 8, (int)rect.Height*8, cs, CGImageAlphaInfo.PremultipliedLast)){
					
					context.SetShadowWithColor(new SizeF(0.0f, 1.0f), 0.7f, UIColor.Black.CGColor);
					context.TranslateCTM(0.0f,0f);
					//context.ScaleCTM(1.0f,-1.0f);
					context.DrawImage(rect,template.CGImage);
					context.SetBlendMode(mode);
					context.ClipToMask(rect,template.CGImage);
					context.SetRGBFillColor(red,green,blue,alpha);
					context.FillRect(rect);				
					
					return UIImage.FromImage (context.ToImage ());
				}
			}
		}

		
        // Child proof the image by rounding the edges of the image
        internal static UIImage RemoveSharpEdges (UIImage image)
        {
			if (image == null)
			{
				Console.WriteLine("throwing error at remove sharp edges");
				throw new ArgumentNullException ("image");
			}
			
            UIGraphics.BeginImageContext (new SizeF (48, 48));
            var c = UIGraphics.GetCurrentContext ();

			c.AddPath (smallPath);
            c.Clip ();

            image.Draw (new RectangleF (0, 0, 48, 48));
            var converted = UIGraphics.GetImageFromCurrentImageContext ();
            UIGraphics.EndImageContext ();
            return converted;
        }
		
		//
		// Centers image, scales and removes borders
		//
		internal static UIImage PrepareForProfileView (UIImage image)
		{
			const int size = 73;
			if (image == null)
			{
				Console.WriteLine("throwing error");
				throw new ArgumentNullException ("image");
			}
			
            UIGraphics.BeginImageContext (new SizeF (73, 73));
            var c = UIGraphics.GetCurrentContext ();

			c.AddPath (largePath);
            c.Clip ();

			// Twitter not always returns squared images, adjust for that.
			var cg = image.CGImage;
			float width = cg.Width;
			float height = cg.Height;
			if (width != height){
				float x = 0, y = 0;
				if (width > height){
					x = (width-height)/2;
					width = height;
				} else {
					y = (height-width)/2;
					height = width;
				}
				c.ScaleCTM (1, -1);
				using (var copy = cg.WithImageInRect (new RectangleF (x, y, width, height))){
					c.DrawImage (new RectangleF (0, 0, size, -size), copy);
				}
			} else 
	            image.Draw (new RectangleF (0, 0, size, size));
			
            var converted = UIGraphics.GetImageFromCurrentImageContext ();
            UIGraphics.EndImageContext ();
            return converted;
		}
		
		internal static CGPath MakeRoundedPath (float size)
		{
			float hsize = size/2;
			
			var path = new CGPath ();
			path.MoveToPoint (size, hsize);
			path.AddArcToPoint (size, size, hsize, size, 4);
			path.AddArcToPoint (0, size, 0, hsize, 4);
			path.AddArcToPoint (0, 0, hsize, 0, 4);
			path.AddArcToPoint (size, 0, size, hsize, 4);
			path.CloseSubpath ();
			
			return path;
		}
		
		public static UIImage newImage(RectangleF rect,UIColor color)
		{
			using (var cs = CGColorSpace.CreateDeviceRGB ()){
				using (var context = new CGBitmapContext (IntPtr.Zero, (int)rect.Width, (int)rect.Height, 8, (int)rect.Height*4, cs, CGImageAlphaInfo.PremultipliedLast)){
					
					rect.X += 5;
					rect.Y += 5;
					rect.Width -= 10;
					rect.Height -= 10;
					
					color.SetColor ();
					context.MoveTo (rect.X,rect.Y);					
					context.AddLineToPoint (rect.X,rect.Height);
					context.AddLineToPoint (rect.Width,rect.Height);
					context.AddLineToPoint (rect.Width,rect.Y);
					context.ClosePath ();
					context.FillPath ();
					
				return UIImage.FromImage (context.ToImage());
				}
			}
		}
		
		public static UIImage ResizeImage(UIImage theImage,float width, float height, bool keepRatio)
		{
			if(keepRatio)
			{
				var ratio = theImage.Size.Height / theImage.Size.Width;
				if(height >0)
					width = height * ratio;
				else 
					height = width * ratio;
			}
			
			
            UIGraphics.BeginImageContext (new SizeF (width,height));
            var c = UIGraphics.GetCurrentContext ();

            theImage.Draw (new RectangleF (0, 0, width, height));
            var converted = UIGraphics.GetImageFromCurrentImageContext ();
            UIGraphics.EndImageContext ();
            return converted;
		}
	}
	
}
