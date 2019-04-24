namespace AllergyFinder.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AllergyFinder.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AllergyFinder.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AllergyFinder.Models.ApplicationDbContext context)
        {
            //context.Allergens.AddOrUpdate(x => x.id,
            //    new Allergen() { id = 1, KnownAllergies = "Wheat" },
            //    new Allergen() { id = 2, KnownAllergies = "Rye" },
            //    new Allergen() { id = 3, KnownAllergies = "Barley" },
            //    new Allergen() { id = 4, KnownAllergies = "Oat" },
            //    new Allergen() { id = 5, KnownAllergies = "Crab" },
            //    new Allergen() { id = 6, KnownAllergies = "Lobster" },
            //    new Allergen() { id = 7, KnownAllergies = "Crayfish" },
            //    new Allergen() { id = 8, KnownAllergies = "Shrimp" },
            //    new Allergen() { id = 9, KnownAllergies = "Crawfish" },
            //    new Allergen() { id = 10, KnownAllergies = "Egg" },
            //    new Allergen() { id = 11, KnownAllergies = "Nut" },
            //    new Allergen() { id = 12, KnownAllergies = "Lactose" },
            //    new Allergen() { id = 13, KnownAllergies = "Milk" },
            //    new Allergen() { id = 14, KnownAllergies = "Almond" },
            //    new Allergen() { id = 15, KnownAllergies = "Hazelnut" },
            //    new Allergen() { id = 16, KnownAllergies = "Pecan" },
            //    new Allergen() { id = 17, KnownAllergies = "Pistachio" },
            //    new Allergen() { id = 18, KnownAllergies = "Macadamia" },
            //    new Allergen() { id = 19, KnownAllergies = "Celery" },
            //    new Allergen() { id = 20, KnownAllergies = "Mustard" },
            //    new Allergen() { id = 21, KnownAllergies = "Sesame" },
            //    new Allergen() { id = 22, KnownAllergies = "Sulphite" },
            //    new Allergen() { id = 23, KnownAllergies = "Lupin" },
            //    new Allergen() { id = 24, KnownAllergies = "Mussel" },
            //    new Allergen() { id = 25, KnownAllergies = "Whelk" },
            //    new Allergen() { id = 26, KnownAllergies = "Oyster" },
            //    new Allergen() { id = 27, KnownAllergies = "Snail" },
            //    new Allergen() { id = 28, KnownAllergies = "Squid" },
            //    new Allergen() { id = 29, KnownAllergies = "Octopus" },
            //    new Allergen() { id = 30, KnownAllergies = "Soy" },
            //    new Allergen() { id = 31, KnownAllergies = "Tuna" },
            //    new Allergen() { id = 32, KnownAllergies = "Salmon" },
            //    new Allergen() { id = 33, KnownAllergies = "Tilapia" },
            //    new Allergen() { id = 34, KnownAllergies = "Cheese" }
            //);

            //context.Reactions.AddOrUpdate(x => x.id,
            //    new Reaction() { id = 1, CommonReactions = "Hives" },
            //    new Reaction() { id = 1, CommonReactions = "Eczema Flareup" },
            //    new Reaction() { id = 1, CommonReactions = "Redness of skin" },
            //    new Reaction() { id = 1, CommonReactions = "Itchy mouth" },
            //    new Reaction() { id = 1, CommonReactions = "Itchy ear canal" },
            //    new Reaction() { id = 1, CommonReactions = "Nausea" },
            //    new Reaction() { id = 1, CommonReactions = "Vomiting" },
            //    new Reaction() { id = 1, CommonReactions = "Diarrhea" },
            //    new Reaction() { id = 1, CommonReactions = "Stomach Pain" },
            //    new Reaction() { id = 1, CommonReactions = "Nasal Congestion" },
            //    new Reaction() { id = 1, CommonReactions = "Runny Nose" },
            //    new Reaction() { id = 1, CommonReactions = "Sneezing" },
            //    new Reaction() { id = 1, CommonReactions = "Coughing" },
            //    new Reaction() { id = 1, CommonReactions = "Odd taste in mouth" },
            //    new Reaction() { id = 1, CommonReactions = "Trouble Swallowing" },
            //    new Reaction() { id = 1, CommonReactions = "Swelling of the Lips,Tongue, or Throat" },
            //    new Reaction() { id = 1, CommonReactions = "Turning Blue" }, //severe
            //    new Reaction() { id = 1, CommonReactions = "Drop in Bllod Pressure" }, //severe
            //    new Reaction() { id = 1, CommonReactions = "Loss of Consciousness" }, //severe
            //    new Reaction() { id = 1, CommonReactions = "Chest Pain" },
            //    new Reaction() { id = 1, CommonReactions = "Weak Pulse" }
            //);
        }
    }
}
