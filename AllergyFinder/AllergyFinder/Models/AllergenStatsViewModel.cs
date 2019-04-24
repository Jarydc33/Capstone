using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllergyFinder.Models
{
    public class AllergenStatsViewModel
    {

        public List<AllergenReactionJunction> allResults = new List<AllergenReactionJunction>();
        public List<AllergenReactionJunction> topResults = new List<AllergenReactionJunction>();
        public int topResultsCount { get; set; }
        public int allResultsCount { get; set; }

    }
}