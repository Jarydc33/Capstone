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
            try
            {
                AllergenTotal total = db.AllergenTotals.Where(a => a.AllergenId == id).FirstOrDefault();
                db.AllergenTotals.Remove(total);
            }
            catch { }
            try
            {
                AllergenJunction user = db.AllergensJunction.Where(a => a.AllergenId == id && a.CustomerId == customer.id).FirstOrDefault();
                db.AllergensJunction.Remove(user);
            }
            catch { }
            try
            {
                List<AllergenReactionJunction> logged = db.AllergensReactionsJunction.Where(a => a.AllergenId == id && a.CustomerId == customer.id).ToList();
                db.AllergensReactionsJunction.RemoveRange(logged);
            }
            catch { }
            
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