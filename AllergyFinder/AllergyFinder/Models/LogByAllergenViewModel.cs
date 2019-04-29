using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllergyFinder.Models
{
    public class LogByAllergenViewModel
    {
        public IEnumerable<SelectListItem> Allergens { get; set; }
        public string[] ChosenAllergens { get; set; }
        public string UserMealName { get; set; }
    }
}