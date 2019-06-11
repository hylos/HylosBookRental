using HylosBookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HylosBookRental.Controllers.Api
{
    public class UsersAPIController : ApiController
    {
        private ApplicationDbContext db;

        public UsersAPIController()
        {
            db = new ApplicationDbContext();
        }

        //To Retrieve Email or Name & Birthdate
        public IHttpActionResult Get(string type, string query=null)
        {
            if (type.Equals("email") && query != null)
            {
                var customerQuery = db.Users.Where(u => u.Email.ToLower().Contains(query.ToLower()));

                //we return the whole list from db since they can be more than one match.
                return Ok(customerQuery.ToList());
            }

            if (type.Equals("name") && query != null)
            {
                var customerQuery = from u in db.Users
                                    where u.Email.Contains(query)
                                    select new { u.FirstName, u.LastName, u.BithDate };

                //we return the whole list from db since they can be more than one match.
                return Ok(customerQuery.ToList()[0].FirstName + " " + customerQuery.ToList()[0].LastName + ";" + customerQuery.ToList()[0].BithDate);
            }

            //if the statement never passes of which it will never happen but we need just to handle it.
            return BadRequest();
        }

        //Disposing db object
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}
