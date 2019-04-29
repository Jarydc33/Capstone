using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AllergyFinder.Models
{
    public class Allergen
    {
        [Key]
        public int id { get; set; }
        public string KnownAllergies { get; set; }
        public bool UserMade { get; set; }
    }
}