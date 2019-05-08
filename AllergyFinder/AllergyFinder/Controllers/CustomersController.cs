using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AllergyFinder.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace AllergyFinder.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            CustomerIndexViewModel customerView = TempData["foods"] as CustomerIndexViewModel;
            return View(customerView);
        }
        [AllowAnonymous]
        public ActionResult Create()
        {
            Customer customerToAdd = new Customer();
            return View(customerToAdd);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(Customer customerToAdd)
        {
            string userId = User.Identity.GetUserId();
            customerToAdd.ApplicationUserId = userId;
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

        public ActionResult LogAllergy()
        {
            AddAllergenViewModel model = new AddAllergenViewModel();
            model.allergens = new SelectList(db.Allergens.ToList(), "id", "KnownAllergies");
            return View(model);
        }

        [HttpPost]
        public ActionResult LogAllergy(AddAllergenViewModel model)
        {
            var customer = GetCustomer();
            AllergenJunction table = new AllergenJunction();
            table = db.AllergensJunction.Where(a => a.CustomerId == customer.id && a.AllergenId == model.id).FirstOrDefault();
            if(table == null)
            {
                table = new AllergenJunction();
                table.AllergenId = model.id;
                table.CustomerId = customer.id;
                db.AllergensJunction.Add(table);
                db.SaveChanges();
            }
            
            AddAllergenViewModel newModel = new AddAllergenViewModel();
            newModel.allergens = new SelectList(db.Allergens.ToList(), "id", "KnownAllergies");
            return View(newModel);
        }

        public Customer GetCustomer()
        {
            string userId = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
            return customer;
        }

        public ActionResult FindFoodItem()
        {
            FindFoodItemViewModel foodToFind = new FindFoodItemViewModel();
            return View(foodToFind);
        }

        public async Task<ActionResult> MenuSearch(int? id)
        {
            var menu = await MenuRetriever.Retrieve(id);
            FindFoodItemViewModel menuItems = new FindFoodItemViewModel();
            menuItems.foundItems = menu;
            return View(menuItems);
        }

        [HttpPost]//<---need to change this at some point
        public async Task<ActionResult> FindFoodItem(FindFoodItemViewModel foodToFind)
        {
            var foodRetrievalRoot = await FoodRetrieval.Retrieve(foodToFind.BrandName,foodToFind.FoodName);
            if(foodRetrievalRoot.list == null)
            {
                TempData["foods"] = null;
                return RedirectToAction("Index");
            }
            var foodRetrieval = foodRetrievalRoot.list.item;
            CustomerIndexViewModel foodView = new CustomerIndexViewModel();
            foodView.retrievedFoods = foodRetrieval;
            TempData["foods"] = foodView;
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> FindFoodInfo(string NDBNo, int route, string mealName)
        {
            var ingredients = await FoodInfoRetrieval.Retrieve(NDBNo);
            //var ingredients = ingredientsRoot.foods[0].food.ing.desc;
            ingredients = ingredients.ToLower();
            List<string> allergensFound = new List<string>();
            if (route == 1)
            {
                //comes here if you click "Log this food" from the index page
                allergensFound = FindAllergens(ingredients, true);
                TempData["foundAllergies"] = allergensFound;
                TempData["MealName"] = mealName;
                return RedirectToAction("LogFood","Customers");
            }
            //comes here if you click "Find Allergens" from the index page
            allergensFound = FindAllergens(ingredients, false);
            TempData["MealName"] = mealName;
            var foodLogAllergens = FindAllergens(ingredients, true);
            FindFoodInfoViewModel model = new FindFoodInfoViewModel();
            string userId = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(u => u.ApplicationUserId == userId).FirstOrDefault();
            var temp = db.AllergensJunction.Where(a => a.CustomerId == customer.id).ToList();
            var placeHolder = new List<Allergen>();
            foreach (var item in temp)
            {
                var y = db.Allergens.Where(a => a.id == item.AllergenId).FirstOrDefault();
                
                placeHolder.Add(y);
            }
            model.allergens = allergensFound.Distinct().ToList();
            model.userAllergies = placeHolder;
            model.toLogAllergens = foodLogAllergens;
            return View(model);
        }

        public List<string> FindAllergens(string ingredients, bool logger)
        {
            var knownAllergens = db.Allergens.ToList();
            var user = GetCustomer();
            var allAllergens = db.Allergens.Where(a => a.CustomerId == null || a.CustomerId == user.id).ToList();
            List<string> allergensFound = new List<string>();
            foreach(var allergen in knownAllergens) 
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

        public ActionResult LogRestaurantFood(int id)
        {
            var menuItem = db.MenuItems.Find(id);
            var menuAllergens = menuItem.Allergens;
            var editedAllergens = FindAllergens(menuAllergens, true);
            TempData["foundAllergens"] = editedAllergens;
            return RedirectToAction("LogFood", new { routeId = -1});
        }

        public ActionResult LogFood(int? routeId) //find a way to break this up
        {
            List<string> editedAllergens = new List<string>();
            string mealName = "";
            //goes this route if logging a beer
            if (routeId > 0)
            {
                BeerClass1[] beers = TempData["BeerIngredients"] as BeerClass1[];
                BeerClass1 selectedBeer = beers.Where(a => a.id == routeId).FirstOrDefault();
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
            //goes this route if logging a non-restaurant item
            else if(routeId == 0 || routeId == null)
            {
                List<string> allergensToLog = TempData["foundAllergies"] as List<string>;
                editedAllergens = allergensToLog.Distinct().ToList();
                mealName = TempData["MealName"] as string;
            }
            //goes this way if logging a restaurant meal or LogByAllergen
            else
            {
                editedAllergens = TempData["foundAllergens"] as List<string>;
                //goes this way if coming from LogByAllergen
                if(routeId == -2)
                {
                    mealName = TempData["MealName"] as string;
                }
                
            }
        
            var customer = GetCustomer();
            FoodLog logger = new FoodLog();
            AllergenTotal logAllergen = new AllergenTotal();
            
            if(editedAllergens == null)
            {
                TempData["foods"] = null;
                return RedirectToAction("Index");
            }

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
                    db.SaveChanges();
                }
                else
                {
                    temp.Total += 1;
                }
            }
            logger.Reactions = null;
            logger.CustomerId = customer.id;
            logger.MealName = mealName;
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
            model.LoggedMeals = new SelectList(db.FoodLogs.Where(f => f.Reactions == null && f.CustomerId == customer.id).ToList(), "id", "MealName");
            model.Reaction = new MultiSelectList(db.Reactions.ToList(),"id", "CommonReactions");
            return View(model);
        }

        public ActionResult LogByAllergen()
        {
            LogByAllergenViewModel model = new LogByAllergenViewModel();
            var user = GetCustomer();
            model.Allergens = new MultiSelectList(db.Allergens.Where(a => a.CustomerId == null || a.CustomerId == user.id).ToList(),"id","KnownAllergies");
            return View(model);
        }

        [HttpPost]
        public ActionResult LogByAllergen(LogByAllergenViewModel model)
        {
            List<string> temp = new List<string>();
            foreach(var item in model.ChosenAllergens)
            {
                int parsed = int.Parse(item);
                temp.Add(db.Allergens.Where(a => a.id == parsed).Select(a => a.KnownAllergies).FirstOrDefault());
                temp.Add(item);
            }
            TempData["foundAllergens"] = temp;
            TempData["MealName"] = model.UserMealName;
            return RedirectToAction("LogFood", new { routeId = -2});
        }

        [HttpPost]
        public ActionResult LogReaction(LogReactionViewModel model)
        {
            var customer = GetCustomer();
            var loggedMeal = db.FoodLogs.Where(l => l.id == model.id).FirstOrDefault();
            List<string> loggedAllergens = loggedMeal.Allergens.Split(',').ToList();
            loggedAllergens.RemoveAt(loggedAllergens.Count - 1);
            loggedMeal.Reactions = "Logged";
            List<int> allergenId = new List<int>();
            AllergenReactionJunction toLog = new AllergenReactionJunction();

            for(int i = 0; i< model.allReactionIds.Length; i++)
            {
                int placeHolder = model.allReactionIds[i];
                ReactionTotal reactionTotal = db.ReactionTotals.Where(r => r.ReactionId == placeHolder).FirstOrDefault();
                if (reactionTotal == null)
                {
                    ReactionTotal newReaction = new ReactionTotal();
                    newReaction.CustomerId = customer.id;
                    newReaction.ReactionId = placeHolder;
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
                    toLog = db.AllergensReactionsJunction.Where(a => a.CustomerId == customer.id && a.ReactionId == placeHolder && a.AllergenId == tempId).FirstOrDefault();
                    if (toLog == null)
                    {
                        AllergenReactionJunction newEntry = new AllergenReactionJunction();
                        newEntry.CustomerId = customer.id;
                        newEntry.ReactionId = placeHolder;
                        newEntry.AllergenId = tempId;
                        newEntry.Total = 1;
                        db.AllergensReactionsJunction.Add(newEntry);
                        db.SaveChanges(); 
                    }                    
                    else
                    {
                        toLog.Total += 1;
                        db.SaveChanges();
                    }
                }
                DetermineAllergy(model.allReactionIds[i]);
            }

            TempData["foods"] = null;
            return RedirectToAction("Index");
        }

        public void DetermineAllergy(int reactionId)
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
            return View(model);
        }

        public ActionResult LogAlcohol()
        {
            LogAlcoholViewModel model = new LogAlcoholViewModel();
            model.Beers = null;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LogAlcohol(LogAlcoholViewModel model)
        {
            model.Beers = await BeerRetriever.Retrieve(model.BeerName);
            return View(model);
        }

        public ActionResult AddCustomAllergen()
        {
            AddCustomAllergenViewModel model = new AddCustomAllergenViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCustomAllergen(AddCustomAllergenViewModel model)
        {
            Allergen toAdd = new Allergen();
            toAdd.KnownAllergies = model.AllergenName;
            Customer user = GetCustomer();
            toAdd.UserMade = true;
            toAdd.CustomerId = user.id;
            db.Allergens.Add(toAdd);
            db.SaveChanges();
            TempData["foods"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult DeleteMealReaction()
        {
            List<FoodLog> loggedMeals = new List<FoodLog>();
            loggedMeals = db.FoodLogs.Where(r => r.Reactions != "Logged").ToList();
            return View(loggedMeals);
        }

        
        public ActionResult DeleteFoodLog(int? id)
        {
            var toDelete = db.FoodLogs.Where(r => r.MealId == id).FirstOrDefault();
            db.FoodLogs.Remove(toDelete);
            db.SaveChanges();
            TempData["foods"] = null;
            return RedirectToAction("Index");
        }

        public void EditJunctionTable(int? reactionId)
        {
            AllergenReactionJunction toChange = new AllergenReactionJunction();
        }

        public ActionResult DeleteCustomAllergen()
        {
            return View();
        }
    }
}
