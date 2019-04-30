using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AllergyFinder.Models;
using Microsoft.AspNet.Identity;

namespace AllergyFinder.Controllers
{
    public class AllergensController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Allergens
        public List<Allergen> GetAllergens()
        {
            List<Allergen> edited = db.Allergens.Where(a => a.UserMade == true).ToList();
            return edited;
        }
        // DELETE: api/Allergens/5
        [ResponseType(typeof(Allergen))]
        public IHttpActionResult DeleteAllergen(int id)
        {
            Allergen allergen = db.Allergens.Find(id);
            string userId = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
            if (allergen == null)
            {
                return NotFound();
            }
            //ADD ERROR CHECKING
            AllergenTotal total = db.AllergenTotals.Where(a => a.AllergenId == id).FirstOrDefault();
            AllergenJunction user = db.AllergensJunction.Where(a => a.AllergenId == id && a.CustomerId == customer.id).FirstOrDefault();
            List<AllergenReactionJunction> logged = db.AllergensReactionsJunction.Where(a => a.AllergenId == id && a.CustomerId == customer.id).ToList();
            foreach(var item in logged)
            {
                db.AllergensReactionsJunction.Remove() //how can I do this? Itll kick back an error cause the list has changed.
            }

            db.AllergensJunction.Remove(user);
            db.AllergenTotals.Remove(total);
            db.Allergens.Remove(allergen);
            db.SaveChanges();

            return Ok(allergen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AllergenExists(int id)
        {
            return db.Allergens.Count(e => e.id == id) > 0;
        }
    }
}