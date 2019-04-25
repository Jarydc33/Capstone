using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AllergyFinder
{
    public class BeerRetriever
    {
        public static Item[] Retrieve(string beerName)
        {
            string editedBeerName = beerName.Replace(" ", "_");
            string strurltest = "https://api.punkapi.com/v2/beers?beer_name=" + beerName;
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

            var beerRequest = JsonConvert.DeserializeObject<FoodInfo>(strresulttest);
            return null;

        }
    }
}