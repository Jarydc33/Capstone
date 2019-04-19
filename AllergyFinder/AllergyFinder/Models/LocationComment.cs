using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AllergyFinder.Models
{
    public class LocationComment
    {
        [Key]
        public int id { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string Comment { get; set; }
    }
}