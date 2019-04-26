using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllergyFinder.Models
{
    public class FindFoodItemViewModel
    {
        public string BrandName { get; set; }
        public string FoodName { get; set; }
        public AddAllergenViewModel Allergens { get; set; }
        public MenuItem[] foundItems { get; set; }
    }
}