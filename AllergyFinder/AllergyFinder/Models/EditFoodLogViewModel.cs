using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllergyFinder.Models
{
    public class EditFoodLogViewModel
    {
        public FoodLog LogToChange { get; set; }
        public IEnumerable<SelectListItem> Reaction { get; set; }
        public int? ReactionId { get; set; }
        public int? FoodLogId { get; set; }
    }
}