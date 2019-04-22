using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllergyFinder.Models
{
    public class FindRestaurantViewModel
    {
        public string RestaurantName { get; set; }
        public string Radius { get; set; }
        public float[] allRestaurants { get; set; }
    }
}