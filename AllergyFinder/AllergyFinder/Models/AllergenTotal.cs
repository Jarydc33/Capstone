﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AllergyFinder.Models
{
    public class AllergenTotal
    {

        [Key]
        public int id { get; set; }

        [ForeignKey("Allergen")]
        public int AllergenId { get; set; }
        public Allergen Allergen { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int Total { get; set; }

    }
}