﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HylosBookRental.Models;
using HylosBookRental.ViewModel;
using HylosBookRental.Utility;

namespace HylosBookRental.Controllers
{ 
    [Authorize(Roles =SD.AdminUserRole)]
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Book
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Genre);
            return View(books.ToList());
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            var model = new BookViewModel
            {
                Book = book,
                Genres = db.Genres.ToList()
            };
            return View(model);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var genre = db.Genres.ToList();
            var model = new BookViewModel
            {
                Genres = genre
            };
            return View(model);
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( BookViewModel bookVM)
        {
            var book = new Book
                {
                     
                    Author = bookVM.Book.Author,
                    Availability = bookVM.Book.Availability,
                    DateAdded = bookVM.Book.DateAdded,
                    Description = bookVM.Book.Description,
                    Genre = bookVM.Book.Genre,
                    GenreId = bookVM.Book.GenreId,
                    ImageUrl = bookVM.Book.ImageUrl,
                    ISBN = bookVM.Book.ISBN,
                    Pages = bookVM.Book.Pages,
                    Price = bookVM.Book.Price,
                    Publisher = bookVM.Book.Publisher,
                    ProductDimensions = bookVM.Book.ProductDimensions,
                    PublicationDate = bookVM.Book.PublicationDate,
                    Title = bookVM.Book.Title
                };

            if (ModelState.IsValid)
                {
                    
                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                bookVM.Genres = db.Genres.ToList();
            
            return View(bookVM);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            var model = new BookViewModel
            {
                Book = book,
                Genres = db.Genres.ToList()
            };

            return View(model);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(BookViewModel bookVM)
        {
            var book = new Book
            {
                Id = bookVM.Book.Id,
                Author = bookVM.Book.Author,
                Availability = bookVM.Book.Availability,
                DateAdded = bookVM.Book.DateAdded,
                Description = bookVM.Book.Description,
                Genre = bookVM.Book.Genre,
                GenreId = bookVM.Book.GenreId,
                ImageUrl = bookVM.Book.ImageUrl,
                ISBN = bookVM.Book.ISBN,
                Pages = bookVM.Book.Pages,
                Price = bookVM.Book.Price,
                Publisher = bookVM.Book.Publisher,
                ProductDimensions = bookVM.Book.ProductDimensions,
                PublicationDate = bookVM.Book.PublicationDate,
                Title = bookVM.Book.Title
            };


            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            bookVM.Genres = db.Genres.ToList();
            return View(book);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var model = new BookViewModel
            {
                Book = book,
                Genres = db.Genres.ToList()
            };

            return View(model);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
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
