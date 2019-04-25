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
        public List<string> allResultsAllergenNames = new List<string>();
        public List<string> allResultsReactionNames = new List<string>();
        public List<int> allResultsCounts = new List<int>();
        public List<double?> allResultsPercentages = new List<double?>();
        //public List<string> topResultsNames = new List<string>();
        //public List<int> topResultsCounts = new List<int>();
        //public List<double?> topResultsPercentages = new List<double?>();
        //public int topResultsCount { get; set; }
        public int allResultsCount { get; set; }

    }
}