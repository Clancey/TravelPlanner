using System;

namespace TravelPlanner
{
	public static class CoordinateExtensions
	{
		public static MonoTouch.CoreLocation.CLLocationCoordinate2D ToNativeCoordinate(this Coordinate c) 
		{
			return new MonoTouch.CoreLocation.CLLocationCoordinate2D(c.Latitude,c.Longitude);
		}
	}
}

