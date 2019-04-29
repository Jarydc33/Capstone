using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AllergyFinder.Models
{
    public class FoodLog
    {

        [Key]
        public int id { get; set; }

        public string Reactions { get; set; }

        public string Allergens { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? MealId { get; set; }

        public string MealName { get; set; }
    }
}