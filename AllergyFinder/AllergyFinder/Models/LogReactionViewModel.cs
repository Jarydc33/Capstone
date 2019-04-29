using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllergyFinder.Models
{
    public class LogReactionViewModel
    {
        [Required(ErrorMessage = "Required.")]
        public IEnumerable<SelectListItem> LoggedMeals { get; set; }
        [Required(ErrorMessage = "Required.")]
        public IEnumerable<SelectListItem> Reaction { get; set; }
        public int id { get; set; }
        public int reactionId { get; set; }
        public int[] allReactionIds { get; set; }
    }
}