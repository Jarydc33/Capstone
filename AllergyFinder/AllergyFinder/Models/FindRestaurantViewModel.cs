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
        public float[] AllRestaurants { get; set; }
        public string CuisineType { get; set; }
        public string[] MenuLink { get; set; }
        public List<LocationComment> Comments { get; set; }
    }
}