using AllergyFinder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AllergyFinder
{
    public class MenuRetriever
    {
        public static MenuItem[] Retrieve(int? restaurantId)
        {
            string strurltest = "http://localhost:59845/api/Menus/" + restaurantId;
            WebRequest requestObject = WebRequest.Create(strurltest);
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

            var foundRestaurant = JsonConvert.DeserializeObject<MenuItem[]>(strresulttest);
            return foundRestaurant;
        }
    }
}