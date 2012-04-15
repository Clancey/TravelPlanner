using System;
using MonoTouch.MapKit;
namespace TravelPlaner
{
	public class EmptyOverlay : MKPolygon
	{
		public MKMapRect boundingMapRect;
		
		MonoTouch.CoreLocation.CLLocationCoordinate2D coordinate;
		public override MonoTouch.CoreLocation.CLLocationCoordinate2D Coordinate {
			get {
				return coordinate;
			}
			//set {
			//	coordinate = value;
			//}
		}
	}
}

