using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AllergyFinder
{
    public class RestaurantSearch
    {
        public static Restaurant[] Retrieve(string search, float? latitude, float? longitude)
        {
            string strurltest;
            string formattedName = search.Replace(" ", "%20");
            strurltest = "https://developers.zomato.com/api/v2.1/search?q=" + search;
            
            
            WebRequest requestObject = WebRequest.Create(strurltest);
            requestObject.Headers.Add("user-key: "+ Keys.ZomatoKey);
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

            var restaurantSearch = JsonConvert.DeserializeObject<Rootobject>(strresulttest);
            return restaurantSearch.restaurants;
        }
    }


    public class Rootobject
    {
        public Restaurant[] restaurants { get; set; }
    }

    public class Restaurant
    {
        public Restaurant1 restaurant { get; set; }
    }

    public class Restaurant1
    {
        public R R { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
       
    }

    public class R
    {
        public int res_id { get; set; }
    }

    public class Location
    {
        public string address { get; set; }
        public string locality { get; set; }
        public string city { get; set; }
        public int city_id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string zipcode { get; set; }
        public int country_id { get; set; }
        public string locality_verbose { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
    }


}