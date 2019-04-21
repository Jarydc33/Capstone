﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AllergyFinder
{
    public static class GetFoodInfo
    {
        public static Item[] Retrieve(string brandName, string foodName)
        {
            string fullBrandName = brandName.Replace(" ", string.Empty);
            string fullFoodName = foodName.Replace(" ", string.Empty);
            string fullName = fullBrandName + "," + fullFoodName;
            string strurltest = "https://api.nal.usda.gov/ndb/search/?format=json&q=" + fullName + "&sort=n&max=25&offset=0&api_key=" + Keys.USDAKey;
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

            var foodRequest = JsonConvert.DeserializeObject<Rootobject>(strresulttest);
            return foodRequest.list.item;
        }
    }


    public class Rootobject
    {
        public List list { get; set; }
    }

    public class List
    {
        public Item[] item { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public string ndbno { get; set; }
    }


}
