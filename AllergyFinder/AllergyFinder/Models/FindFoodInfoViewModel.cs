using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllergyFinder.Models
{
    public class FindFoodInfoViewModel
    {
        public List<string> allergens { get; set; }
        public List<string> toLogAllergens { get; set; }
        public List<Allergen> userAllergies { get; set; }
    }
}