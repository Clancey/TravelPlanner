using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Net;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using System.Xml;

namespace TravelPlanner
{


	public class DataAccess
	{

		public static TravelPlanner.TravelTicker.TravelTickerSearchResults FetchTravelTickerResults (string url)
		{
			string formattedUri = String.Format (CultureInfo.InvariantCulture, url);
			string jsonResponse = GetWebsiteData (formattedUri);
			var result = TravelTickerParser.Parse (jsonResponse);
			return result;
		}
				
		public static TravelPlanner.HotelSearch.HotelSearchResults FetchHotelSearchResults (string url)
		{
			Console.WriteLine(url);
			string formattedUri = String.Format (CultureInfo.InvariantCulture, url);
			string jsonResponse = GetWebsiteData (formattedUri);
			var result = HotelSearchParser.Parse (jsonResponse);
			return result;
		}

		private static string GetWebsiteData (string formattedUri)
		{
			Uri address = new Uri (formattedUri);
			
			HttpWebRequest request = WebRequest.Create (address) as HttpWebRequest;
			
			string responseBody = null;
			try {
				//UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
				
				using (HttpWebResponse response = request.GetResponse () as HttpWebResponse) {
					var reader = new StreamReader (response.GetResponseStream (), Encoding.UTF8);
					
					responseBody = reader.ReadToEnd ();
				}
			} catch (WebException we) {
				Console.WriteLine (we.Message);
			} finally {
				//UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			}
			return responseBody;
		}
		
		
		
	}
#if WINDOWS_PHONE
    public static class WebRequestExtensions
    {
        public static WebResponse GetResponse(this WebRequest request)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            IAsyncResult asyncResult = request.BeginGetResponse(r => autoResetEvent.Set(), null);

            // Wait until the call is finished
            autoResetEvent.WaitOne();

            return request.EndGetResponse(asyncResult);
        }

        public static Stream GetRequestStream(this WebRequest request)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            IAsyncResult asyncResult = request.BeginGetRequestStream(r => autoResetEvent.Set(), null);

            // Wait until the call is finished
            autoResetEvent.WaitOne();

            return request.EndGetRequestStream(asyncResult);
        }
    }
#endif

}
