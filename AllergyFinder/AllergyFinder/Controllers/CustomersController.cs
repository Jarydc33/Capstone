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
            return View();
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
            string userId = User.Identity.GetUserId();
            Customer user = db.Customers.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
            AllergenJunction table = new AllergenJunction();
            table.AllergenId = model.id;
            table.CustomerId = user.id;
            db.AllergensJunction.Add(table);
            db.SaveChanges();
            return RedirectToAction("Index");
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
            GetFoodInfo.Retrieve(foodToFind.BrandName,foodToFind.FoodName);
            return RedirectToAction("Index");
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
