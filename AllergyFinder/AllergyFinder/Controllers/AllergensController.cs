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
            if (allergen == null)
            {
                return NotFound();
            }

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