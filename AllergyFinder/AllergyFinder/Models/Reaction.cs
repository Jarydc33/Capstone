using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AllergyFinder.Models
{
    public class Reaction
    {
        [Key]
        public int id { get; set; }
        public string CommonReactions { get; set; }
    }
}