using System;
using AllergyFinder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AllergyFinder.Controllers.CommentsAPI
{
    [RoutePrefix("api/comments")]
    public class CommentsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public List<LocationComment> GetComments()
        {
            var comments = db.LocationComments.ToList();
            return comments;
        }
        
    }
}
