using System;
using System.Device.Location;
using System.Net;
using System.Linq;
using System.Xml.Linq;
using System.Threading;

namespace KarmaloopAI.Skills
{
    public static class LocationSkills
    {
        private static GeoCoordinateWatcher watcher;
        static string location = string.Empty;
        static string lat;
        static string longi;
        static string baseUri = "http://maps.googleapis.com/maps/api/" + "geocode/xml?latlng={0},{1}&sensor=false";
        static string blankResponse = "Hey! it seems like I am unable to track your current location.\nAnything else where i can help you?";

        public static SkillResult GetLocation(SkillParams skillParams)
        {
            //string location = string.Empty;
            getGeoCoordinates();
            Thread.Sleep(5000);
            RetrieveFormatedAddress(lat, longi);
            Thread.Sleep(5000);
            if(location != null)
            return new SkillResult(location);
            else
                return new SkillResult(blankResponse);
        }

        static public void getGeoCoordinates()
        {
            watcher = new GeoCoordinateWatcher();
            watcher.StatusChanged += watcher_StatusChanged;
            watcher.TryStart(false, TimeSpan.FromSeconds(3));
        }

        static void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            try
            {
                if (e.Status == GeoPositionStatus.Ready)
                {
                    if (watcher.Position.Location.IsUnknown)
                    {
                        //this.txtUserAddress.Text = "Cannot find data";
                    }
                    else
                    {
                        GeoCoordinate location = watcher.Position.Location;
                        lat = location.Latitude.ToString();
                        longi = location.Longitude.ToString();
                        
                    }
                }
                else { }
            }
            catch (Exception ex) { }
        }

        static void RetrieveFormatedAddress(string lat, string lng)
        {
            string requestUri = string.Format(baseUri, lat, lng);
            using (WebClient wc = new WebClient())
            {
                wc.DownloadStringCompleted += wc_DownloadStringCompleted;
                wc.DownloadStringAsync(new Uri(requestUri));
            }
        }

        static void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                var xmlElm = XElement.Parse(e.Result);

                var status = (from elm in xmlElm.Descendants()
                              where elm.Name == "status"
                              select elm).FirstOrDefault();
                if (status.Value.ToLower() == "ok")
                {
                    var res = (from elm in xmlElm.Descendants()
                               where elm.Name == "formatted_address"
                               select elm).FirstOrDefault();
                    location = res.Value.ToString();
                }
                else { }
            }
            catch (Exception) { }
        }
    }
}
