using Newtonsoft.Json;
using AllergyFinder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AllergyFinder
{
    public class GetComments
    {
        public static LocationComment[] Retrieve()
        {
            string strurltest = "http://localhost:59845/api/comments";
            List<LocationComment> test = new List<LocationComment>();
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

            var commentRequest = JsonConvert.DeserializeObject<LocationComment[]>(strresulttest);
            return commentRequest;
        }
    }
    
}