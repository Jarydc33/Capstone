using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AllergyFinder
{
    public static class CitySearch
    {
        public static float Retrieve(float? latitude, float? longitude)
        {
            string strurltest = "https://developers.zomato.com/api/v2.1/geocode?lat=" + latitude + "&lon=" + longitude;


            WebRequest requestObject = WebRequest.Create(strurltest);
            requestObject.Headers.Add("user-key: " + Keys.ZomatoKey);
            requestObject.Method = "GET";
            HttpWebResponse responseObject = null;
            responseObject = (HttpWebResponse)requestObject.GetResponse();

            string strresulttest = null;
            using (Stream stream = responseObject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresulttest = sr.ReadToEnd();
                sr.Close();
            }

            var CityId = JsonConvert.DeserializeObject<CityObject>(strresulttest);
            return CityId.location.city_id;
        }
    }


    public class CityObject
    {
        public Location location { get; set; }
    }

}