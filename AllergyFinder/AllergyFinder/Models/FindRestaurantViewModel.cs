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
        public string[] Comments { get; set; }
        public int? RestaurantId { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
    }
}