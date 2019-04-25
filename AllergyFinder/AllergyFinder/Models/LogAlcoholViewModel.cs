using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllergyFinder.Models
{
    public class LogAlcoholViewModel
    {
        public string BeerName { get; set; }
        public BeerClass1[] Beers;
    }
}