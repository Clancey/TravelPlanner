using System;
using System.Drawing;
using System.Linq;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using System.Globalization;
using ClanceysLib;
using MonoTouch.Foundation;
using MonoTouch.Dialog.Utilities;
namespace Hotwire
{
	public class TravelTickerDealElement : Element ,IImageUpdated, IElementSizing
	{
		public static NSString key = new NSString("travelTickerDealElement");
		public Uri ImageUri;
		public UIImage Image;
		public UIImage PriceImage;
		Hotwire.TravelTicker.TravelTickerDeal Deal;
		public TravelTickerDealElement (Hotwire.TravelTicker.TravelTickerDeal deal) : base (deal.Title)
		{
			Deal = deal;
			if(Deal.Images.Count == 0)
			{
				//Console.WriteLine("use car");
				this.Image = UITheme.car;
			}
			else
			{
				this.ImageUri = new Uri(Deal.Images.Last());
				this.Image = ImageLoader.DefaultRequestImage (ImageUri, this);
			}
			
			
		}
		
		public override MonoTouch.UIKit.UITableViewCell GetCell (DialogViewController dvc, MonoTouch.UIKit.UITableView tv)
		{
			var cell = tv.DequeueReusableCell(key) as DealCellView;
			//if(cell == null)
				cell = new DealCellView(this);
			cell.SelectionStyle = UITableViewCellSelectionStyle.Gray;
			if(this.Image == null)
				this.Image = ImageLoader.DefaultRequestImage (ImageUri, this);
			return cell;
		}
		
		public override void Selected (DialogViewController dvc, UITableView tableView, NSIndexPath path)
		{
			dvc.ActivateController(new TravelTickerDetailViewController(Deal));
		}

		#region IImageUpdated implementation
		public void UpdatedImage (Uri uri)
		{
			
			if (uri == null)
				return;
			if(uri.AbsoluteUri != Deal.Images.Last())
			{
				Console.WriteLine("WTF");	
			}
			
			this.Image = ImageLoader.DefaultRequestImage (ImageUri, this);
			this.Reload();
		}
		#endregion
		
		public virtual float GetHeight (UITableView tableView, NSIndexPath indexPath)
		{
			float height = 0;
			
			var frame = tableView.Frame;;
			frame.Width -= (DealCellView.Padding + DealCellView.ImageSpace + DealCellView.Padding + DealCellView.leftImageSpace + DealCellView.Padding + DealCellView.Padding);
			
			var text = Deal.Title;
			if (string.IsNullOrEmpty (text))
				text = "";
			SizeF size = new SizeF (frame.Width, float.MaxValue);
			height = tableView.StringSize (text, DealCellView.font, size, UILineBreakMode.WordWrap).Height;
			if (height <= 12)
				height = 17;
			height += 55;
			return height > 65f ? height : 65f;
		}
		
		
		public class DealCellView : UITableViewCell
		{
			public static UIFont font = UIFont.BoldSystemFontOfSize (12);
			public static UIFont subFont = UIFont.SystemFontOfSize (12);
			TravelTickerDealElement parent;
			UILabel label;
			UILabel lblSub;
			UIView ImageView;
			DealView leftView;
			public const int ImageSpace = 55;
			public const int leftImageSpace = 60;
			public const int imageViewWidth = 55;
			public const int imageViewHeight = 55;
			public const int Padding = 8;
			UIAlertView callAV;

			public DealCellView (TravelTickerDealElement parent) : base(UITableViewCellStyle.Value1, key)
			{
				this.parent = parent;
				//this.BackgroundView = new UIView { BackgroundColor = parent.backColor () };
				
				label = new UILabel { TextAlignment = UITextAlignment.Left, Text = parent.Caption, Lines = 0, Font = font, LineBreakMode = UILineBreakMode.WordWrap, BackgroundColor = UIColor.Clear };
				lblSub = new UILabel { TextAlignment = UITextAlignment.Left, Text = parent.Deal.ShortDetails, Font = subFont, BackgroundColor = UIColor.Clear };
				UpdateLeftImage(parent.Deal);
				UpdateRightImage();
				
				ContentView.Add (label);
				ContentView.Add (lblSub);
				ContentView.Add (ImageView);
				ContentView.Add (leftView);
			}
			private void UpdateRightImage()
			{
				UIImage img = parent.Image;
				if(img != null)
					ImageView = new UIImageView( img);//Graphics.PrepareForProfileView(img));
				else
				{
					ImageView = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.Gray);
					((UIActivityIndicatorView)ImageView).StartAnimating ();
				}
			}


			void UpdateLeftImage (Hotwire.TravelTicker.TravelTickerDeal deal)
			{
				if(leftView == null)
					leftView = new DealView(deal);
				else
					leftView.Update(deal);
			}

			public override void LayoutSubviews ()
			{
				base.LayoutSubviews ();
				var full = ContentView.Bounds;
				var frame = full;
				frame.X = Padding + leftImageSpace + Padding;
				frame.Width -= (DealCellView.ImageSpace + DealCellView.Padding + DealCellView.leftImageSpace + DealCellView.Padding + DealCellView.Padding);
				float height = 0;
				var text = parent.Caption;
				if (string.IsNullOrEmpty (text))
					text = "";
				SizeF size = new SizeF (frame.Width, float.MaxValue);
				height = this.StringSize (text, font, size, UILineBreakMode.WordWrap).Height;
				if (height <= 12)
					height = 17;
				frame.Height = height + 10;
				
				
				frame.Y = 5;
				label.Frame = frame;
				var subFrame = frame;
				subFrame.Y += frame.Height;
				subFrame.Height = 17;
				subFrame.Width -= imageViewWidth + Padding;
				lblSub.Frame = subFrame;
				var imageFrame = subFrame;
				leftView.Frame = new RectangleF (0, (full.Height - leftImageSpace)/2, leftImageSpace + Padding, leftImageSpace);
				
				ImageView.Frame = new RectangleF (full.Width - ImageSpace - Padding, (full.Height - ImageSpace) / 2, ImageSpace, ImageSpace);
			}

			
		}
		
		public class DealView : UIView 
		{
			public UILabel Price{get;set;}
			public UILabel Disclaimer {get;set;}
			private UILabel Detail {get;set;}
			
			public DealView(Hotwire.TravelTicker.TravelTickerDeal deal)
			{
				Hotwire.TravelTicker.DealOfferType DealOffer = deal.DealOffers.DealOffer[0];
				Price = new UILabel();
				Price.Font = UIFont.BoldSystemFontOfSize(20f);
				Price.TextColor = UIColor.FromRGB(165,0,1);
				Price.TextAlignment = UITextAlignment.Center;
				Price.Text = Double.Parse(DealOffer.FromPrice.Amount.ToString()).ToString("C0", CultureInfo.CreateSpecificCulture("en-US"));
				Disclaimer = new UILabel();
				Disclaimer.Text = DealOffer.PriceQualificationCode;
				
				Detail = new UILabel();
				Detail.Text = "Continue";
				
				this.AddSubview(Price);
				this.AddSubview(Disclaimer);
				this.AddSubview(Detail);
			}
			public override void LayoutSubviews ()
			{
				base.LayoutSubviews ();
				var frame = this.Frame;
				var priceFrame = frame;
				priceFrame.Height = this.StringSize (Price.Text, Price.Font, frame.Size, UILineBreakMode.WordWrap).Height;
				priceFrame.Y = (frame.Height - priceFrame.Height) /2;
				Price.Frame = priceFrame;
				
				
				
			}
			
			public void Update(Hotwire.TravelTicker.TravelTickerDeal deal)
			{
					
			}
			
			
		}
	}
	
	public class TravelTickerThemeElement : StringElement
	{
		Hotwire.TravelTicker.Theme Theme;
		public TravelTickerThemeElement(Hotwire.TravelTicker.Theme theme) : base (theme.ThemeName)
		{
			Theme = theme;
		}
		public override void Selected (DialogViewController dvc, UITableView tableView, NSIndexPath path)
		{
			base.Selected (dvc, tableView, path);
			var url = Constants.TravelTickerUrl + "&theme=" + Theme.ThemeId;
			dvc.ActivateController(new TravelTickerViewController(url,Theme.ThemeName,true));	
		}
	}
	
	public class HotelResultElement : Element, IElementSizing
	{
		public static NSString key = new NSString("hotelDealElement");
		public Uri ImageUri;
		public UIImage Image;
		Hotwire.HotelSearch.HotelResult Deal;
		public HotelResultElement (Hotwire.HotelSearch.HotelResult deal) : base (deal.Title)
		{
			Deal = deal;
		}
		
		public override MonoTouch.UIKit.UITableViewCell GetCell (DialogViewController dvc, MonoTouch.UIKit.UITableView tv)
		{
			var cell = tv.DequeueReusableCell(key) as DealCellView;
			if(cell == null)
				cell = new DealCellView(this);
			else
				cell.Update(this);
			cell.SelectionStyle = UITableViewCellSelectionStyle.Gray;
			return cell;
		}
		
		public override void Selected (DialogViewController dvc, UITableView tableView, NSIndexPath path)
		{
			dvc.ActivateController(new HotelDetailView(Deal));
		}
		
		public virtual float GetHeight (UITableView tableView, NSIndexPath indexPath)
		{
			float height = 0;
			
			var frame = tableView.Frame;;
			frame.Width -= (DealCellView.Padding + DealCellView.ImageSpace + DealCellView.Padding + DealCellView.leftImageSpace + DealCellView.Padding + DealCellView.Padding);
			
			var text = Deal.Title;
			if (string.IsNullOrEmpty (text))
				text = "";
			SizeF size = new SizeF (frame.Width, float.MaxValue);
			height = tableView.StringSize (text, DealCellView.font, size, UILineBreakMode.WordWrap).Height;
			if (height <= 12)
				height = 17;
			height += 55;
			return height > 65f ? height : 65f;
		}
		
		
		public class DealCellView : UITableViewCell
		{
			public static UIFont font = UIFont.BoldSystemFontOfSize (12);
			public static UIFont subFont = UIFont.SystemFontOfSize (12);
			HotelResultElement Parent;
			UILabel label;
			UILabel lblSub;
			UIView ImageView;
			DealView leftView;
			public const int ImageSpace = 5;
			public const int leftImageSpace = 60;
			public const int imageViewWidth = 55;
			public const int imageViewHeight = 55;
			public const int Padding = 8;
			UIAlertView callAV;

			public DealCellView (HotelResultElement parent) : base(UITableViewCellStyle.Value1, key)
			{
				this.Parent = parent;
				//this.BackgroundView = new UIView { BackgroundColor = parent.backColor () };
				
				label = new UILabel { TextAlignment = UITextAlignment.Left, Text = parent.Caption, Lines = 0, Font = font, LineBreakMode = UILineBreakMode.WordWrap, BackgroundColor = UIColor.Clear };
				lblSub = new UILabel { TextAlignment = UITextAlignment.Left, Text = parent.Deal.Neighborhood.Description, Font = subFont, BackgroundColor = UIColor.Clear };
				UpdateLeftImage(parent.Deal);
				ContentView.Add (label);
				ContentView.Add (lblSub);
				ContentView.Add (leftView);
			}
			
			void UpdateLeftImage (Hotwire.HotelSearch.HotelResult deal)
			{
				if(leftView == null)
					leftView = new DealView(deal);
				else
					leftView.Update(deal);
			}

			public override void LayoutSubviews ()
			{
				base.LayoutSubviews ();
				var full = ContentView.Bounds;
				var frame = full;
				frame.X = Padding + leftImageSpace + Padding;
				frame.Width -= (DealCellView.ImageSpace + DealCellView.Padding + DealCellView.leftImageSpace + DealCellView.Padding + DealCellView.Padding);
				float height = 0;
				var text = Parent.Caption;
				if (string.IsNullOrEmpty (text))
					text = "";
				SizeF size = new SizeF (frame.Width, float.MaxValue);
				height = this.StringSize (text, font, size, UILineBreakMode.WordWrap).Height;
				if (height <= 12)
					height = 17;
				frame.Height = height + 10;
				
				
				frame.Y = 5;
				label.Frame = frame;
				var subFrame = frame;
				subFrame.Y += frame.Height;
				subFrame.Height = 17;
				//subFrame.Width -= imageViewWidth + Padding;
				lblSub.Frame = subFrame;
				var imageFrame = subFrame;
				leftView.Frame = new RectangleF (0, (full.Height - leftImageSpace)/2, leftImageSpace + Padding, leftImageSpace);
			
			}
			
			public void Update(HotelResultElement parent)
			{
				Parent = parent;
				label.Text = parent.Caption;
				lblSub.Text = parent.Deal.Neighborhood.Description;
				UpdateLeftImage(parent.Deal);
			}

			
		}
		
	}
	
	public class DealView : UIView 
	{
		public UILabel Price{get;set;}
		public UILabel Disclaimer {get;set;}
		private StarView StarView {get;set;}
		
		public DealView (RectangleF rect, Hotwire.HotelSearch.HotelResult deal) : base(rect)
		{
			Price = new UILabel();
			Price.Font = UIFont.BoldSystemFontOfSize(20f);
			Price.TextColor = UIColor.FromRGB(165,0,1);
			Price.TextAlignment = UITextAlignment.Center;
			Price.Text = deal.AveragePricePerNight.ToString("C0", CultureInfo.CreateSpecificCulture("en-US"));
			Price.BackgroundColor = UIColor.Clear;
			Disclaimer = new UILabel();
			Disclaimer.Text = "per room per night*";
			
			StarView = new StarView(deal.StarRating);
			
			this.AddSubview(Price);
			this.AddSubview(Disclaimer);
			this.AddSubview(StarView);
		}
		
		public DealView(Hotwire.HotelSearch.HotelResult deal) : this(RectangleF.Empty,deal)
		{
		
		}
	
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			var frame = this.Frame;
			var priceFrame = frame;
			priceFrame.Height = this.StringSize (Price.Text, Price.Font, frame.Size, UILineBreakMode.WordWrap).Height;
			priceFrame.Y = (frame.Height - priceFrame.Height) /2;
			Price.Frame = priceFrame;
			var starFrame = frame;
			starFrame.Y = priceFrame.AbsoluteHeight();
			starFrame.Height -= starFrame.Y;
			StarView.Frame = starFrame;
		}
		
		public void Update(Hotwire.HotelSearch.HotelResult deal)
		{
			Price.Text = deal.SubTotal.ToString("C0", CultureInfo.CreateSpecificCulture("en-US"));
		}
	}
	
	
	public class HotelAmenityElement : StringElement
	{
		Hotwire.HotelSearch.Amenity Amenity;
		public HotelAmenityElement(Hotwire.HotelSearch.Amenity amenity) : base (amenity.Name)
		{
			Amenity = amenity;
		}
		public override void Selected (DialogViewController dvc, UITableView tableView, NSIndexPath path)
		{
			base.Selected (dvc, tableView, path);
			//var url = Constants.TravelTickerUrl + "&theme=" + Amenity.ThemeId;
			//dvc.ActivateController(new TravelTickerViewController(url,Amenity.ThemeName,true));	
		}
	}
}

