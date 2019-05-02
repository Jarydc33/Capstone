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

        protected override void Seed(ApplicationDbContext context)
        {
            //context.Allergens.AddOrUpdate(x => x.id,
            //    new Allergen() { id = 1, KnownAllergies = "Wheat", UserMade = false },
            //    new Allergen() { id = 2, KnownAllergies = "Rye", UserMade = false },
            //    new Allergen() { id = 3, KnownAllergies = "Barley", UserMade = false },
            //    new Allergen() { id = 4, KnownAllergies = "Oat", UserMade = false },
            //    new Allergen() { id = 5, KnownAllergies = "Crab", UserMade = false },
            //    new Allergen() { id = 6, KnownAllergies = "Lobster", UserMade = false },
            //    new Allergen() { id = 7, KnownAllergies = "Crayfish", UserMade = false },
            //    new Allergen() { id = 8, KnownAllergies = "Shrimp", UserMade = false },
            //    new Allergen() { id = 9, KnownAllergies = "Crawfish", UserMade = false },
            //    new Allergen() { id = 10, KnownAllergies = "Egg", UserMade = false },
            //    new Allergen() { id = 11, KnownAllergies = "Nut", UserMade = false },
            //    new Allergen() { id = 12, KnownAllergies = "Lactose", UserMade = false },
            //    new Allergen() { id = 13, KnownAllergies = "Milk", UserMade = false },
            //    new Allergen() { id = 14, KnownAllergies = "Almond", UserMade = false },
            //    new Allergen() { id = 15, KnownAllergies = "Hazelnut", UserMade = false },
            //    new Allergen() { id = 16, KnownAllergies = "Pecan", UserMade = false },
            //    new Allergen() { id = 17, KnownAllergies = "Pistachio", UserMade = false },
            //    new Allergen() { id = 18, KnownAllergies = "Macadamia", UserMade = false },
            //    new Allergen() { id = 19, KnownAllergies = "Celery", UserMade = false },
            //    new Allergen() { id = 20, KnownAllergies = "Mustard", UserMade = false },
            //    new Allergen() { id = 21, KnownAllergies = "Sesame", UserMade = false },
            //    new Allergen() { id = 22, KnownAllergies = "Sulphite", UserMade = false },
            //    new Allergen() { id = 23, KnownAllergies = "Lupin", UserMade = false },
            //    new Allergen() { id = 24, KnownAllergies = "Mussel", UserMade = false },
            //    new Allergen() { id = 25, KnownAllergies = "Whelk", UserMade = false },
            //    new Allergen() { id = 26, KnownAllergies = "Oyster", UserMade = false },
            //    new Allergen() { id = 27, KnownAllergies = "Snail", UserMade = false },
            //    new Allergen() { id = 28, KnownAllergies = "Squid", UserMade = false },
            //    new Allergen() { id = 29, KnownAllergies = "Octopus", UserMade = false },
            //    new Allergen() { id = 30, KnownAllergies = "Soy", UserMade = false },
            //    new Allergen() { id = 31, KnownAllergies = "Tuna", UserMade = false },
            //    new Allergen() { id = 32, KnownAllergies = "Salmon", UserMade = false },
            //    new Allergen() { id = 33, KnownAllergies = "Tilapia", UserMade = false },
            //    new Allergen() { id = 34, KnownAllergies = "Cheese", UserMade = false },
            //    new Allergen() { id = 31, KnownAllergies = "Hops", UserMade = false },
            //    new Allergen() { id = 32, KnownAllergies = "Malt", UserMade = false },
            //    new Allergen() { id = 33, KnownAllergies = "Yeast", UserMade = false }
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

            //context.Restaurants.AddOrUpdate(x => x.id,
            //     new Restaurant() { Name = "Burger King", RestaurantId = 1 },
            //     new Restaurant() { Name = "McDonalds", RestaurantId = 2 }

            //    );

            //context.MenuItems.AddOrUpdate(x => x.id,
            //    new MenuItem() { RestaurantId = 1, FoodItem = "Double Stacker KING", Allergens = "egg, milk, soy, wheat" },
            //    new MenuItem() { RestaurantId = 1, FoodItem = "Angry WHOPPER", Allergens = "egg, milk, soy, wheat" },
            //    new MenuItem() { RestaurantId = 1, FoodItem = "Chicken Garden Salad", Allergens = "egg, milk, wheat" },
            //    new MenuItem() { RestaurantId = 2, FoodItem = "Big Mac", Allergens = "egg, milk, soy, wheat" },
            //    new MenuItem() { RestaurantId = 2, FoodItem = "Caramel Frappe", Allergens = "milk" },
            //    new MenuItem() { RestaurantId = 2, FoodItem = "Chicken McNuggets", Allergens = "wheat, sulphite" }
            //    );
        }
    }
}
