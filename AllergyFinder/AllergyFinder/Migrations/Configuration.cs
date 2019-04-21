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
            //    new Allergen() { id = 1, KnownAllergies = "Gluten" },
            //    new Allergen() { id = 2, KnownAllergies = "Shellfish" },
            //    new Allergen() { id = 3, KnownAllergies = "Eggs" },
            //    new Allergen() { id = 1, KnownAllergies = "Fish" },
            //    new Allergen() { id = 2, KnownAllergies = "Peanuts" },
            //    new Allergen() { id = 3, KnownAllergies = "Soybeans" },
            //    new Allergen() { id = 1, KnownAllergies = "Milk" },
            //    new Allergen() { id = 2, KnownAllergies = "Nuts" },
            //    new Allergen() { id = 3, KnownAllergies = "Celery" },
            //    new Allergen() { id = 1, KnownAllergies = "Mustard" },
            //    new Allergen() { id = 2, KnownAllergies = "Sesame" },
            //    new Allergen() { id = 2, KnownAllergies = "Sulphites" },
            //    new Allergen() { id = 3, KnownAllergies = "Lupin" },
            //    new Allergen() { id = 1, KnownAllergies = "Molluscs" }
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
