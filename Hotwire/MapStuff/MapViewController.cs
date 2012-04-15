using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.MapKit;
using System.Threading;
using ClanceysLib;
using MonoTouch.Foundation;
using System.Linq;
using MonoTouch.CoreLocation;
using System.Drawing;
using MapStuff.DrawMap;
using MonoTouch.ObjCRuntime;
using System.Xml.Linq;
using MonoTouch.CoreGraphics;
namespace Hotwire
{
	public class MapViewController : UIViewController
	{

		public List<MKPolygon> neighborhoods = new List<MKPolygon> ();
		public MKMapView map;
		public UISegmentedControl switcher;
		public MBProgressHUD progress;
		public MKMapRect maxRect;
		public bool LockMap = false;		
		private bool showAllNeighborhoods;
		private string NeighborhoodId;
		
		public MapViewController ()
		{
			showAllNeighborhoods = true;
			init();
		}
		
		public MapViewController (string neighborhoodId)
		{
			showAllNeighborhoods = false;
			NeighborhoodId = neighborhoodId;
			init();
		}
		
		private void init()
		{
			map = new MKMapView ();
			map.Frame = this.View.Bounds;
			map.MapType = MKMapType.Standard;
			map.Region = getMapBounds ();
			//map.SetVisibleMapRect(new MKMapRect(46286327.3006886 , 107394439.844582,12713.8773526251 , 8475.9182350859),false);
			maxRect = map.visibleMapRect;
			map.Delegate = new MapViewDelegate (this);
			map.ShowsUserLocation = true;
			

			this.View.AddSubview (map);
			addNeighborHoods();
		}

		public override void ViewDidAppear (bool animated)
		{
			//this.NavigationItem.TitleView.AddSubview(switcher);
			base.ViewDidAppear (animated);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public MKCoordinateRegion getMapBounds ()
		{
			if(showAllNeighborhoods)
			{
				var maxLat = HotelSearchParser.results.Neighborhoods.SelectMany(x=> x.Shape.Select(y=> y.Latitude)).Max();
				var minLat = HotelSearchParser.results.Neighborhoods.SelectMany(x=> x.Shape.Select(y=> y.Latitude)).Min();
				var maxLong = HotelSearchParser.results.Neighborhoods.SelectMany(x=> x.Shape.Select(y=> y.Longitude)).Max();
				var minLong = HotelSearchParser.results.Neighborhoods.SelectMany(x=> x.Shape.Select(y=> y.Longitude)).Min();
				var width = (maxLat - minLat);
				if(width < 0)
					width *=-1;
				var height =  maxLong - minLong;
				if(height < 0)
					height *=-1;
				return new MKCoordinateRegion ( new CLLocationCoordinate2D (((maxLat - minLat) / 2) + minLat,((maxLong - minLong)/2) + minLong), new MKCoordinateSpan (width, height));
			}
			else
			{
				var neighborHood = HotelSearchParser.results.Neighborhoods.Where(x=> x.Id == NeighborhoodId).FirstOrDefault();
				var maxLat = neighborHood.Shape.Select(x=> x.Latitude).Max();
				var minLat = neighborHood.Shape.Select(x=> x.Latitude).Min();
				var maxLong = neighborHood.Shape.Select(x=> x.Longitude).Max();
				var minLong = neighborHood.Shape.Select(x=> x.Longitude).Min();
				var width = (maxLat - minLat);
				if(width < 0)
					width *=-1;
				var height =  maxLong - minLong;
				if(height < 0)
					height *=-1;
				return new MKCoordinateRegion (neighborHood.Centeroid, new MKCoordinateSpan (width, height));
			}
			
		}


		public void addNeighborHoods()
		{
			if(showAllNeighborhoods)
			{
				foreach(var neighborhood in HotelSearchParser.results.Neighborhoods)
					map.AddOverlay (MKPolygon.FromCoordinates (neighborhood.Shape.ToArray()));
			}
			else
				map.AddOverlay(MKPolygon.FromCoordinates(HotelSearchParser.results.Neighborhoods.Where(x=> x.Id == NeighborhoodId).FirstOrDefault().Shape.ToArray()));
		}


		public class MyAnnotation : MKAnnotation
		{
			private CLLocationCoordinate2D _coordinate;
			private string _title, _subtitle;
			public override CLLocationCoordinate2D Coordinate {
				get { return _coordinate; }
				set { _coordinate = value; }
			}
			public override string Title {
				get { return _title; }
			}
			public override string Subtitle {
				get { return _subtitle; }
			}
			public long NodeId { get; set; }
			/// <summary>
			/// Need this constructor to set the fields, since the public
			/// interface of this class is all READ-ONLY
			/// <summary>
			public MyAnnotation (CLLocationCoordinate2D coord, string t, string s, long nodeID) : base()
			{
				_coordinate = coord;
				_title = t;
				_subtitle = s;
				NodeId = nodeID;
			}
			
		}

		class MapViewDelegate : MKMapViewDelegate
		{
			MapViewController _viewController;

			MKMapRect lastGoodMapRect;
			bool manuallyChangingMapRect;

			public MapViewDelegate (MapViewController viewController)
			{
				_viewController = viewController;
			}

			public override void RegionWillChange (MKMapView mapView, bool animated)
			{
				if (manuallyChangingMapRect)
					return;
				lastGoodMapRect = mapView.visibleMapRect;
			}
			/*
				if (_viewController.currentAnnotationView != null)
				{
					_viewController.currentAnnotationView.Hidden = true;
				}
				*/
			public bool MKMapRectContainsRect (MKMapRect firstRect, MKMapRect secondRect)
			{
				var rect2 = (new Rectangle ((int)secondRect.MinX, (int)secondRect.MinY, (int)(secondRect.MaxX - secondRect.MinX), (int)(secondRect.MaxY - secondRect.MinY)));
				return (new Rectangle ((int)firstRect.MinX, (int)firstRect.MinY, (int)(firstRect.MaxX - firstRect.MinX), (int)(firstRect.MaxY - firstRect.MinY))).Contains (rect2);
			}
			public override void RegionChanged (MKMapView mapView, bool animated)
			{
				if (manuallyChangingMapRect || !_viewController.LockMap)
					//prevents possible infinite recursion when we call setVisibleMapRect below
					return;
				
				bool mapContainsOverlay = MKMapRectContainsRect (mapView.visibleMapRect, _viewController.maxRect);
				
				if (mapContainsOverlay)
				{
					// The overlay is entirely inside the map view but adjust if user is zoomed out too much...
					double widthRatio = _viewController.maxRect.Size.Width / mapView.visibleMapRect.Size.Width;
					double heightRatio = _viewController.maxRect.Size.Height / mapView.visibleMapRect.Size.Height;
					//adjust ratios as needed
					if ((widthRatio < 0.6) || (heightRatio < 0.6))
					{
						manuallyChangingMapRect = true;
						mapView.SetVisibleMapRect (_viewController.maxRect, true);
						manuallyChangingMapRect = false;
					}
				}

			}


			public override MKOverlayView GetViewForOverlay (MKMapView mapView, NSObject overlay)
			{
				MKOverlayView overlayView = null;
				if (overlay is MKPolygon)
				{
					var polygon = (MKPolygon)overlay;
					var polygonView = new MKPolygonView (polygon);
					polygonView.FillColor = UITheme.IconColor;
					polygonView.Alpha = .3f;
					polygonView.StrokeColor = UITheme.IconColor;
					polygonView.LineWidth = 3;
					overlayView = polygonView;
				}


				else if (overlay is MKPolyline)
				{
					var polylineView = new MKPolylineView ((MKPolyline)overlay);
					polylineView.FillColor = UIColor.Red;
					polylineView.StrokeColor = UIColor.Red;
					polylineView.LineJoin = MonoTouch.CoreGraphics.CGLineJoin.Round;
					polylineView.LineDashPattern = new NSNumber[] { new NSNumber (1), new NSNumber (1) };
					polylineView.LineWidth = 3;
					overlayView = polylineView;
				}
				return overlayView;
			}
			public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, NSObject annotation)
			{
				MKAnnotationView annotationView = null;
				
				/*
				if (annotation is MyAnnotation)
				{
					var pin = annotation as MyAnnotation;
					var pinView = new MKAnnotationView (pin, "colors");
					var section = Updates.AttractionSections.Where (x => x.Key == pin.NodeId).Select (y => y.Value).FirstOrDefault () ?? "";
					
					
					pinView.RightCalloutAccessoryView = UIButton.FromType (UIButtonType.DetailDisclosure);
					((UIButton)pinView.RightCalloutAccessoryView).TouchDown += delegate {
						Console.WriteLine("I was touched!");	
					};//.AddTarget (this, new Selector ("openSpot:"), UIControlEvent.TouchUpInside);
					pinView.Enabled = true;
					pinView.CanShowCallout = true;
					pinView.CenterOffset = new PointF (8, -13);
					pinView.CalloutOffset = new PointF (-8, 0);
					pinView.Image = string.IsNullOrEmpty (section) ? Images.GrayPin : Updates.SectionColors[section];
					annotationView = pinView;
					
				}
				else if (annotation is PersonAnnotation)
				{
					var person = annotation as PersonAnnotation;
					var view = new MKAnnotationView (person, "person");
					view.Image = person.Image(view.StringSize(person.PartyMember.DisplayName,person.Font));
					//view.SetSelected(true,false);
					view.Enabled = true;
					view.CanShowCallout = true;
					view.RightCalloutAccessoryView = UIButton.FromType (UIButtonType.DetailDisclosure);
					((UIButton)view.RightCalloutAccessoryView).TouchDown += delegate {
						Console.WriteLine("I was touched!");
						var alert = new UIAlertView("Are you sure?","Do you want to send " + person.PartyMember.DisplayName + " and update request?",null,"No Thanks","Yes");
						alert.Dismissed += delegate(object sender, UIButtonEventArgs e) {
							if(e.ButtonIndex > 0){
								Settings.ShowNetwork();
							}
						};
						alert.Show();	 
					};
					
					annotationView = view;
					
				}
				else if (annotation is RestaurantAnnotation)
				{
					var resaurant = annotation as RestaurantAnnotation;
					var view = new MKAnnotationView (resaurant, "resaurant");
					view.Image = Images.Restaurant;
					//view.SetSelected(true,false);
					view.Enabled = true;
					view.CanShowCallout = true;
					annotationView = view;
				}
				else if (annotation is RestroomAnnotation)
				{
					var restroom = annotation as RestroomAnnotation;
					var view = new MKAnnotationView (restroom, "restroom");
					view.Image = Images.Restroom;
					//view.SetSelected(true,false);
					view.Enabled = true;
					//view.CanShowCallout = true;
					annotationView = view;
				}
				else if (annotation is GeometryAnnotation || annotation is MergedGeometryAnnotation)
				{
					Console.WriteLine ("it's a line class");
					
					//GeometryAnnotation geometryAnnotation = annotation as GeometryAnnotation;
					
					LinePolygonAnnotationView _annotationView = new LinePolygonAnnotationView (new RectangleF (0, 0, mapView.Frame.Size.Width, mapView.Frame.Size.Height));
					_annotationView.Annotation = annotation;
					_annotationView.MapView = mapView;
					
					
					//_viewController.currentAnnotationView = _annotationView;
					annotationView = _annotationView;
				}
				
				*/
				return annotationView;
			//	return base.GetViewForAnnotation(mapView,annotation);
			}
			public override void MapLoaded (MKMapView mapView)
			{
			}
		}
			/*
				foreach(var person in _viewController.People)
					mapView.SelectAnnotation(person,false);
					*/			
			}
}

