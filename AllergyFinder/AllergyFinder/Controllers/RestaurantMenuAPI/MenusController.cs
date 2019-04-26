using AllergyFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AllergyFinder.Controllers.RestaurantMenuAPI
{
    public class MenusController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Menus
        [HttpGet]
        public List<Restaurant> Get()
        {
            return null;
        }

        // GET: api/Menus/5
        public List<MenuItem> Get(int id)
        {
            var menu = db.MenuItems.Where(r => r.RestaurantId == id).ToList();
            return menu;
        }

        // POST: api/Menus
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Menus/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Menus/5
        public void Delete(int id)
        {
        }
    }
}
