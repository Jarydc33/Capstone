using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AllergyFinder.Models
{
    public class Restaurant
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public int? RestaurantId { get; set; }
    }
}