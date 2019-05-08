using AllergyFinder.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllergyFinder.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Admin";
                    return RedirectToAction("Index", "Administrators");
                }
                else if (isCustomerUser())
                {
                    ViewBag.displayMenu = "Customer";
                    return RedirectToAction("Index", "Customers");
                }
            }
            return View();
        }
               
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var getRole = UserManager.GetRoles(user.GetUserId());
                if (getRole[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public Boolean isCustomerUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext db = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                var getRole = UserManager.GetRoles(user.GetUserId());
                if (getRole[0].ToString() == "Customer")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}