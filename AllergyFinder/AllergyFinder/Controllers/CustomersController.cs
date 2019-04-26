using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AllergyFinder.Models;
using Microsoft.AspNet.Identity;

namespace AllergyFinder.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            CustomerIndexViewModel customerView = TempData["foods"] as CustomerIndexViewModel;
            return View(customerView);
        }

        public ActionResult Create()
        {
            Customer customerToAdd = new Customer();
            return View(customerToAdd);
        }
        [HttpPost]
        public ActionResult Create(Customer customerToAdd)
        {
            string userId = User.Identity.GetUserId();
            customerToAdd.ApplicationUserId = userId;
            float[] coordinates = GeoCoder.GetLatLong(customerToAdd);
            customerToAdd.Latitude = coordinates[0];
            customerToAdd.Longitude = coordinates[1];
            customerToAdd.City_Id = CitySearch.Retrieve(customerToAdd.Latitude, customerToAdd.Longitude).ToString(); //make sure to change this when they edit their profile
            db.Customers.Add(customerToAdd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", customer.ApplicationUserId);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", customer.ApplicationUserId);
            return View(customer);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult LeaveComment()
        {
            LocationComment newComment = new LocationComment();
            return View(newComment);
        }

        [HttpPost]
        public ActionResult LeaveComment(LocationComment commentToAdd)
        {
            db.LocationComments.Add(commentToAdd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult LogAllergy()
        {
            AddAllergenViewModel model = new AddAllergenViewModel();
            model.allergens = new SelectList(db.Allergens.ToList(), "id", "KnownAllergies");
            return View(model);
        }

        [HttpPost]
        public ActionResult LogAllergy(AddAllergenViewModel model)
        {
            string userId = User.Identity.GetUserId();
            Customer user = db.Customers.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
            AllergenJunction table = new AllergenJunction();
            table.AllergenId = model.id;
            table.CustomerId = user.id;
            db.AllergensJunction.Add(table);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FindRestaurant()
        {
            FindRestaurantViewModel search = new FindRestaurantViewModel();
            return View(search);
        }

        [HttpPost]
        public ActionResult FindRestaurant(FindRestaurantViewModel search)
        {
            
            string userId = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
            var restaurants = RestaurantSearch.Retrieve(search.RestaurantName, customer.City_Id,search.Radius,search.CuisineType);
            float[] temp = new float[(restaurants.Length) * 2];
            string[] menuTemp = new string[restaurants.Length];
            string[] commentsTemp = new string[restaurants.Length];
            var comments = GetComments.Retrieve();
            for(int i = 0,j=0; i < restaurants.Length; i++,j+=2)
            {
                temp[j] = float.Parse(restaurants[i].restaurant.location.latitude);
                temp[j+1] = float.Parse(restaurants[i].restaurant.location.longitude);
                menuTemp[i] = restaurants[i].restaurant.menu_url;
                foreach(var comment in comments)
                {
                    if(comment.Latitude == temp[j] && comment.Longitude == temp[j + 1])
                    {
                        commentsTemp[i] = comment.Comment;
                    }
                    else
                    {
                        commentsTemp[i] = "No Comments Saved";
                    }
                }
            }
            search.AllRestaurants = temp;
            search.MenuLink = menuTemp;
            search.Comments = commentsTemp;
            return View(search);
        }

        public ActionResult FindFoodItem()
        {
            FindFoodItemViewModel foodToFind = new FindFoodItemViewModel();
            AddAllergenViewModel allAllergens = new AddAllergenViewModel();
            foodToFind.Allergens = allAllergens;
            foodToFind.Allergens.allergens = new SelectList(db.Allergens.ToList(),"id","KnownAllergies");
            return View(foodToFind);
        }

        public ActionResult MenuSearch(FindFoodItemViewModel restaurantModel)
        {
            var searchRestaurantId = db.Restaurants.Where(r => r.Name == restaurantModel.BrandName).Select(r => r.RestaurantId).FirstOrDefault();
            if(searchRestaurantId == null)
            {
                return RedirectToAction("FindFoodItem");
            }
            restaurantModel.foundItems = MenuRetriever.Retrieve(searchRestaurantId);
            return View(restaurantModel);
        }

        [HttpPost]
        public ActionResult FindFoodItem(FindFoodItemViewModel foodToFind)
        {
            var foodRetrieval = FoodRetrieval.Retrieve(foodToFind.BrandName,foodToFind.FoodName);
            CustomerIndexViewModel foodView = new CustomerIndexViewModel();
            foodView.retrievedFoods = foodRetrieval;
            TempData["foods"] = foodView;
            return RedirectToAction("Index");
        }

        public ActionResult FindFoodInfo(string NDBNo, int route)
        {
            var ingredients = FoodInfoRetrieval.Retrieve(NDBNo);
            ingredients = ingredients.ToLower();
            List<string> allergensFound = new List<string>();
            if (route == 1)
            {
                allergensFound = FindAllergens(ingredients, true);
                TempData["foundAllergies"] = allergensFound;
                return RedirectToAction("LogFood","Customers");
            }
            allergensFound = FindAllergens(ingredients, false);
            FindFoodInfoViewModel model = new FindFoodInfoViewModel();
            string userId = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(u => u.ApplicationUserId == userId).FirstOrDefault();
            AllergenJunction temp = db.AllergensJunction.Where(a => a.CustomerId == customer.id).FirstOrDefault();
            try
            {
                model.userAllergies = db.Allergens.Where(a => a.id == temp.AllergenId).ToList();
            }
            catch
            {

            }
            model.allergens = allergensFound.Distinct().ToList();
            return View(model);
        }

        public List<string> FindAllergens(string ingredients, bool logger)
        {
            var knownAllergens = db.Allergens.ToList();
            List<string> allergensFound = new List<string>();
            foreach(var allergen in knownAllergens) //fix this to not go through whole list?
            {
                if (ingredients.Contains(allergen.KnownAllergies.ToLower()))
                {
                    allergensFound.Add(allergen.KnownAllergies);
                    if (logger)
                    {
                        string allergyId = db.Allergens.Where(a => a.KnownAllergies == allergen.KnownAllergies).Select(a => a.id).FirstOrDefault().ToString();
                        allergensFound.Add(allergyId);
                    }
                    
                }
            }
            return allergensFound;
        }

        public ActionResult LogFood(int? beerId) //should I keep a db of all past meals? Also, give the option of viewing ingredients and THEN logging the food
        {
            List<string> editedAllergens = new List<string>();
            if (beerId != 0)
            {
                BeerClass1[] beers = TempData["BeerIngredients"] as BeerClass1[];
                BeerClass1 selectedBeer = beers.Where(a => a.id == beerId).FirstOrDefault();
                string ingredients = "";
                if (selectedBeer.ingredients.hops != null)
                {
                    ingredients += "hops, ";
                }
                if(selectedBeer.ingredients.malt != null)
                {
                    ingredients += "malt, ";
                }
                if(selectedBeer.ingredients.yeast != null)
                {
                    ingredients += "yeast, ";
                }
                editedAllergens = FindAllergens(ingredients, true);
            }
            else
            {
                List<string> allergensToLog = TempData["foundAllergies"] as List<string>;
                editedAllergens = allergensToLog.Distinct().ToList();
            }            
                                  
            string userId = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(u => u.ApplicationUserId == userId).FirstOrDefault();
            FoodLog logger = new FoodLog();
            AllergenTotal logAllergen = new AllergenTotal();
            

            for(int i = 0, j = 1; i < editedAllergens.Count; i += 2, j+=2)
            {
                logger.Allergens += editedAllergens[i] + ",";
                int tempId = int.Parse(editedAllergens[j]);
                var temp = db.AllergenTotals.Where(a => a.CustomerId == customer.id && a.AllergenId == tempId).FirstOrDefault();
                if(temp == null)
                {
                    logAllergen.CustomerId = customer.id;
                    logAllergen.AllergenId = tempId;
                    logAllergen.Total = 1;
                    db.AllergenTotals.Add(logAllergen);
                    db.SaveChanges(); // change to async
                }
                else
                {
                    temp.Total += 1;
                }
            }
            logger.Reactions = null;
            logger.CustomerId = customer.id;
            logger.MealId = db.FoodLogs.Where(f => f.CustomerId == customer.id).Max(m => m.MealId);
            if(logger.MealId == null)
            {
                logger.MealId = 1;
            }
            else
            {
                logger.MealId++;
            }

            db.FoodLogs.Add(logger);
            db.SaveChanges();
            TempData["foods"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult LogReaction()
        {
            string userId = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(u => u.ApplicationUserId == userId).FirstOrDefault();
            LogReactionViewModel model = new LogReactionViewModel();
            model.LoggedMeals = new SelectList(db.FoodLogs.Where(f => f.Reactions == null && f.CustomerId == customer.id).ToList(), "id", "MealId");
            model.Reaction = new SelectList(db.Reactions.ToList(),"id", "CommonReactions");
            return View(model);
        }

        [HttpPost]
        public ActionResult LogReaction(LogReactionViewModel model)
        {            
            string userId = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(u => u.ApplicationUserId == userId).FirstOrDefault();
            var loggedMeal = db.FoodLogs.Where(l => l.id == model.id).FirstOrDefault();
            List<string> loggedAllergens = loggedMeal.Allergens.Split(',').ToList();
            loggedAllergens.RemoveAt(loggedAllergens.Count - 1);
            loggedMeal.Reactions = "Logged";
            List<int> allergenId = new List<int>();
            AllergenReactionJunction toLog = new AllergenReactionJunction();

            ReactionTotal reactionTotal = db.ReactionTotals.Where(r => r.ReactionId == model.reactionId).FirstOrDefault();
            if (reactionTotal == null)
            {
                ReactionTotal newReaction = new ReactionTotal();
                newReaction.CustomerId = customer.id;
                newReaction.ReactionId = model.reactionId;
                newReaction.Total = 1;
                db.ReactionTotals.Add(newReaction);
                db.SaveChanges();
            }
            else
            {
                reactionTotal.Total += 1;
                db.SaveChanges();
            }

            foreach (var allergen in loggedAllergens)
            {
                int tempId = db.Allergens.Where(a => a.KnownAllergies == allergen).Select(a => a.id).FirstOrDefault();
                toLog = db.AllergensReactionsJunction.Where(a => a.CustomerId == customer.id && a.ReactionId == model.reactionId && a.AllergenId == tempId).FirstOrDefault();
                if(toLog == null)
                {
                    AllergenReactionJunction newEntry = new AllergenReactionJunction();
                    newEntry.CustomerId = customer.id;
                    newEntry.ReactionId = model.reactionId;
                    newEntry.AllergenId = tempId;
                    newEntry.Total = 1;
                    db.AllergensReactionsJunction.Add(newEntry);
                    db.SaveChanges(); //change to async
                }
                else
                {
                    toLog.Total += 1;
                    db.SaveChanges();
                }
            }
            DetermineAllergy(model.reactionId);
            TempData["foods"] = null;
            return RedirectToAction("Index");
        }

        public void DetermineAllergy(int reactionId) //this will eventually change to a list of ints
        {
            ReactionTotal lookupReactionTotal = db.ReactionTotals.Where(r => r.ReactionId == reactionId).FirstOrDefault();
            var reactions = db.AllergensReactionsJunction.Where(a => a.ReactionId == reactionId).ToList();
            List<float> percentages = new List<float>();
            foreach(var item in reactions)
            {

                double temp = Math.Round((double)item.Total / lookupReactionTotal.Total,3);
                temp *= 100;
                item.Percentage = temp;
                db.SaveChanges();
            }
        }

        public ActionResult AllergenStats()
        {
            AllergenStatsViewModel model = new AllergenStatsViewModel();
            string test = "";
            var tempAllResults = db.AllergensReactionsJunction.ToList();
            string[] allResults = new string[tempAllResults.Count];
            foreach (var item in tempAllResults)
            {
                item.Allergen = db.Allergens.Where(a => a.id == item.AllergenId).FirstOrDefault();
                string reaction = db.Reactions.Where(r => r.id == item.ReactionId).Select(r => r.CommonReactions).FirstOrDefault();
                test = item.Allergen.KnownAllergies + " has a " + item.Percentage + "% chance of causing " + reaction;
                model.results.Add(test);
            }

            //List<AllergenReactionJunction> topResults = new List<AllergenReactionJunction>();
            //int numReactions = db.ReactionTotals.Count();
            //var reactionIds = db.ReactionTotals.Select(r => r.ReactionId).Distinct().ToList();

            //foreach(var reaction in reactionIds)
            //{
            //    var maxValue = db.AllergensReactionsJunction.Where(a => a.ReactionId == reaction).Max(a => a.Percentage);
            //    topResults = db.AllergensReactionsJunction.Where(a => a.ReactionId == reaction && a.Percentage == maxValue).ToList();
            //}
            
            //model.allResults = tempAllResults;
            ////model.topResults = topResults;
            //model.allResultsCount = tempAllResults.Count;
            //model.results = re;
            //model.topResultsCount = topResults.Count;
            return View(model);
        }

        public ActionResult LogAlcohol()
        {
            LogAlcoholViewModel model = new LogAlcoholViewModel();
            model.Beers = null;
            return View(model);
        }

        [HttpPost]
        public ActionResult LogAlcohol(LogAlcoholViewModel model)
        {
            model.Beers = BeerRetriever.Retrieve(model.BeerName);
            return View(model);
        }
       
    }
}
