using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AllergyFinder
{
    public class BeerRetriever
    {
        public static Task<BeerClass1[]> Retrieve(string beerName)
        {
            string editedBeerName = beerName.Replace(" ", "_");
            string strurltest = "https://api.punkapi.com/v2/beers?beer_name=" + beerName;
            WebRequest requestObject = WebRequest.Create(strurltest);
            requestObject.Method = "GET";
            HttpWebResponse responseObject = (HttpWebResponse)requestObject.GetResponse();

            string strresulttest = null;
            using (Stream stream = responseObject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresulttest = sr.ReadToEnd();
                sr.Close();
            }

            return Task.Run(() =>
                JsonConvert.DeserializeObject<BeerClass1[]>(strresulttest)
            );
        }

        //public async static Task<BeerClass1[]> Retrieve(string beerName)
        //{
        //    string url = "https://api.punkapi.com/v2/beers?beer_name=" + beerName;

        //    var strresulttest = await GetAPI(url);

        //    var test = JsonConvert.DeserializeObject<BeerClass1[]>(strresulttest);
        //    return test;
        //}

        //public static Task<string> GetAPI(string strurltest)
        //{
        //    WebRequest requestObject = WebRequest.Create(strurltest);
        //    requestObject.Method = "GET";
        //    HttpWebResponse responseObject = null;
        //    responseObject = (HttpWebResponse)requestObject.GetResponse();

        //    string strresulttest = null;
        //    using (Stream stream = responseObject.GetResponseStream())
        //    {
        //        StreamReader sr = new StreamReader(stream);
        //        strresulttest = sr.ReadToEnd();
        //        sr.Close();
        //    }
        //    return Task.Run(() =>
        //        strresulttest
        //    );
        //}
    }

    public class BeerClass1
    {
        public int id { get; set; }
        public string name { get; set; }        
        public Ingredients ingredients { get; set; }
    }

    public class Ingredients
    {
        public Malt[] malt { get; set; }
        public Hop[] hops { get; set; }
        public string yeast { get; set; }
    }

    public class Malt
    {
        public string name { get; set; }
    }
    public class Hop
    {
        public string name { get; set; }
    }


}
