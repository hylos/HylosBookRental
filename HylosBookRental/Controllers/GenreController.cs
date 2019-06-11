using HylosBookRental.Models;
using HylosBookRental.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HylosBookRental.Controllers
{
    [Authorize(Roles = SD.AdminUserRole)]
    public class GenreController : Controller
    {
        //Init db object
        private ApplicationDbContext db;

        //Constructor
        public GenreController()
        {
            //connect to db
            db = new ApplicationDbContext();
        }


        // GET: Genre
        public ActionResult Index()
        {
             
            return View(db.Genres.ToList());
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // Post: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();

                //We redirect to the action method so that we can see the added Genre
                return RedirectToAction("Index");
           
            }
            return View();
        }

        //Details view method
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Genre genre = db.Genres.Find(id);
            if(genre == null)
            {
                return HttpNotFound();
            }

            return View(genre);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                //use this method if you're updating one or two coloumn(s).
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Genre genre = db.Genres.Find(id);
            db.Genres.Remove(genre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Dispose connection string
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}