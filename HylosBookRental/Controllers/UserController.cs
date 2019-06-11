using HylosBookRental.Models;
using HylosBookRental.Utility;
using HylosBookRental.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HylosBookRental.Controllers
{
    [Authorize(Roles = SD.AdminUserRole)]
    public class UserController : Controller
    {
        private ApplicationDbContext db;

        //Constructor
        public UserController()
        {
            db = ApplicationDbContext.Create();
        }

        // GET: User
        public ActionResult Index()
        {
            var user = from u in db.Users
                       join m in db.MembershipTypes on u.MembershipTypeId equals m.Id
                       select new UserViewModel
                       {
                           Id = u.Id,
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           Email = u.Email,
                           Phone = u.Phone,
                           BithDate = u.BithDate,
                           MembershipTypeId = u.MembershipTypeId,
                           MemebershipTypes = (ICollection<MembershipType>)db.MembershipTypes.ToList().Where(n => n.Id.Equals(u.MembershipTypeId)),
                           Disable = u.Disable
                       };

            var usersList = user.ToList();

            return View(usersList);
        }

        //Get: Edit
        //my method will take string type id because its a user id
        public ActionResult Edit(string id)
        {
            //Check if id is null

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //
            ApplicationUser user = db.Users.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }

            UserViewModel model = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BithDate = user.BithDate,
                Email = user.Email,
                Id = user.Id,
                MembershipTypeId = user.MembershipTypeId,
                MemebershipTypes = db.MembershipTypes.ToList(),
                Phone = user.Phone,
                Disable = user.Disable
            };

            return View(model);
        }

        //Post: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                UserViewModel model = new UserViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BithDate = user.BithDate,
                    Email = user.Email,
                    Id = user.Id,
                    MembershipTypeId = user.MembershipTypeId,
                    MemebershipTypes = db.MembershipTypes.ToList(),
                    Phone = user.Phone,
                    Disable = user.Disable
                };

                return View("Edit", model);
            }
            else
            {
                var userInDb = db.Users.Single(u => u.Id == user.Id);
                userInDb.FirstName = user.FirstName;
                userInDb.LastName = user.LastName;
                userInDb.Email = user.Email;
                userInDb.BithDate = user.BithDate;
                userInDb.MembershipTypeId = user.MembershipTypeId;
                userInDb.Phone = user.Phone;
                userInDb.Disable = user.Disable;
            }

            db.SaveChanges();
            return RedirectToAction("Index", "User");
        }

        //Get: Details
        public ActionResult Details(string id)
        {
            if (id == null || id.Length == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = db.Users.Find(id);

            //Coverting application user into view model

            UserViewModel model = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BithDate = user.BithDate,
                Email = user.Email,
                Id = user.Id,
                MembershipTypeId = user.MembershipTypeId,
                MemebershipTypes = db.MembershipTypes.ToList(),
                Phone = user.Phone,
                Disable = user.Disable
            };

            return View(model);
        }

        //Get: Delete
        public ActionResult Delete(string id)
        {
            if (id == null || id.Length == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = db.Users.Find(id);

            //Coverting application user into view model

            UserViewModel model = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BithDate = user.BithDate,
                Email = user.Email,
                Id = user.Id,
                MembershipTypeId = user.MembershipTypeId,
                MemebershipTypes = db.MembershipTypes.ToList(),
                Phone = user.Phone,
                Disable = user.Disable
            };

            return View(model);
        }

        //Post: Delete
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult  DeleteConfirmed(string id)
        {
            /*
             * I will not delete the record of the user but
             * I will the Disable flag to true.
             * Admin will still have the record os the user even if they decide to terminate their contract but the user will be disabled.
             */
            var userInDb = db.Users.Find(id);
            if (id == null || id.Length == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            userInDb.Disable = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}