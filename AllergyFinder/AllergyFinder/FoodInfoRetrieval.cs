using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AllergyFinder
{
    public static class FoodInfoRetrieval
    {
        public static string Retrieve(string NDBNo)
        {
            string strurltest = "https://api.nal.usda.gov/ndb/V2/reports?ndbno=" + NDBNo + "&type=b&format=json&api_key=" + Keys.USDAKey;
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

            var foodRequest = JsonConvert.DeserializeObject<USDAFoodInfo>(strresulttest);
            return foodRequest.foods[0].food.ing.desc; 
        }
    }


    public class USDAFoodInfo
    {
        public Food[] foods { get; set; }
    }

    public class Food
    {
        public Food1 food { get; set; }
    }

    public class Food1
    {
        public Ing ing { get; set; }
        public object[] footnotes { get; set; }
    }

    public class Ing
    {
        public string desc { get; set; }
    }


}