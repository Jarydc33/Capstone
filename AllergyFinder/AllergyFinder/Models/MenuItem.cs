using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AllergyFinder.Models
{
    public class MenuItem
    {
        [Key]
        public int id { get; set; }
        public string FoodItem { get; set; }
        public string Allergens { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}