using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllergyFinder.Models
{
    public class AddAllergenViewModel
    {
        public IEnumerable<SelectListItem> allergens { get; set; }
        public int id { get; set; }
    }
}