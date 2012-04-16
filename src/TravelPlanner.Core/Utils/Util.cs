using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TravelPlanner
{
	public static class Util
	{	
		public static string GetPropertyValue (object inObject, string propertyName)
		{
			PropertyInfo[] props = inObject.GetType ().GetProperties ();
			PropertyInfo prop = props.Select (p => p).Where (p => p.Name == propertyName).FirstOrDefault ();
			if (prop != null)
				return prop.GetValue (inObject, null).ToString ();
			return "";
		}
		
		public static object[] GetPropertyArray (object inObject, string propertyName)
		{
			PropertyInfo[] props = inObject.GetType ().GetProperties ();
			PropertyInfo prop = props.Select (p => p).Where (p => p.Name == propertyName).FirstOrDefault ();
			if (prop != null)
			{
				var currentObject = prop.GetValue (inObject, null);
				if (currentObject.GetType ().GetGenericTypeDefinition () == typeof(List<>))
				{
					return (new ArrayList ((IList)currentObject)).ToArray ();
				}

				else if (currentObject is Array)
				{
					return (object[])currentObject;
				}
				else
				{
					return new object[1];
				}
			}
			return new object[1];
		}
	}
}

