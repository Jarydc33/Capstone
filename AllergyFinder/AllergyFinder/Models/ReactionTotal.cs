using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AllergyFinder.Models
{
    public class ReactionTotal
    {

        [Key]
        public int id { get; set; }

        [ForeignKey("Reaction")]
        public int ReactionId { get; set; }
        public Reaction Reaction { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int Total { get; set; }

    }
}