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
            for(int i = 0,j=0; i < restaurants.Length; i++,j+=2)
            {
                temp[j] = float.Parse(restaurants[i].restaurant.location.latitude);
                temp[j+1] = float.Parse(restaurants[i].restaurant.location.longitude);
                menuTemp[i] = restaurants[i].restaurant.menu_url;
            }
            search.AllRestaurants = temp;
            search.MenuLink = menuTemp;
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
        [HttpPost]
        public ActionResult FindFoodItem(FindFoodItemViewModel foodToFind)
        {
            var foodRetrieval = FoodRetrieval.Retrieve(foodToFind.BrandName,foodToFind.FoodName);
            CustomerIndexViewModel foodView = new CustomerIndexViewModel();
            foodView.retrievedFoods = foodRetrieval;
            TempData["foods"] = foodView;
            return RedirectToAction("Index");
        }

        public ActionResult FindFoodInfo(string NDBNo)
        {
            var ingredients = FoodInfoRetrieval.Retrieve(NDBNo);
            ingredients = ingredients.ToLower();
            var allergensFound = FindAllergens(ingredients);
            FindFoodInfoViewModel model = new FindFoodInfoViewModel();
            string userId = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(u => u.ApplicationUserId == userId).FirstOrDefault();
            AllergenJunction temp = db.AllergensJunction.Where(a => a.CustomerId == customer.id).FirstOrDefault();
            model.userAllergies = db.Allergens.Where(a => a.id == temp.AllergenId).ToList();
            model.allergens = allergensFound.Distinct().ToList();
            return View(model);
        }

        public List<string> FindAllergens(string ingredients)
        {
            var knownAllergens = db.Allergens.ToList();
            List<string> allergensFound = new List<string>();
            foreach(var allergen in knownAllergens)
            {
                if (ingredients.Contains(allergen.KnownAllergies.ToLower()))
                {
                    allergensFound.Add(allergen.KnownAllergies);
                }
            }
            return allergensFound;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
