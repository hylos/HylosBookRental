using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using HylosBookRental.Models;
using Microsoft.AspNet.Identity;
using HylosBookRental.Utility;
using HylosBookRental.ViewModel;

namespace HylosBookRental.Controllers
{
    public class BookDetailController : Controller
    {
        //Create variable for db access
        private ApplicationDbContext db;

        //Intiatialize db variable
        public BookDetailController()
        {
            db = ApplicationDbContext.Create();
        }

        // GET: BookDetail
        public ActionResult Index(int id)
        {
            //Get user id
            //it retrieve users id who's currently log in
            var userid = User.Identity.GetUserId();

            //retriev all properties of the user in the database 
            var user = db.Users.FirstOrDefault(u => u.Id == userid);

            //Create a book model variable which will include all the book details and Genre for that particular book.
            //books and genre are joined together by the Include statement based on primary and foreign key attribute.
            var bookModel = db.Books.Include(b => b.Genre).SingleOrDefault(b => b.Id == id);

            var rentalPrice = 0.0;
            var oneMonthRental = 0.0;
            var sixMonthRental = 0.0;

            //admin and users won't be able to know or to see the prices if they are not logged in because we firstly need to know which membership they are subscribed to.
            if (user != null && !User.IsInRole(SD.AdminUserRole))
            {
                //will use linq to join users and membership types tables
                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes on u.MembershipTypeId equals m.Id
                                 where u.Id.Equals(userid)
                                 select new { m.ChargeRateOneMonth, m.ChargeRateSixMonth };
                
                //Pricing Calculations
                oneMonthRental = Convert.ToDouble(bookModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneMonth) / 100;
                sixMonthRental = Convert.ToDouble(bookModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateSixMonth) / 100;

            }

            //Now we need to pass to the model
            //But here will also have to join from multiple tables because some properties are coming from different tables.

            BookRentalViewModel model = new BookRentalViewModel
            {
                BookId =bookModel.Id,
                ISBN = bookModel.ISBN,
                Author = bookModel.Author,
                Availability = bookModel.Availability,
                DateAdded = bookModel.DateAdded,
                Description = bookModel.Description,
                //will have to find the genres in the db based on the id
                Genre = db.Genres.FirstOrDefault(g=>g.Id.Equals(bookModel.GenreId)),
                GenreId = bookModel.GenreId,
                ImageUrl = bookModel.ImageUrl,
                Pages = bookModel.Pages,
                Price = bookModel.Price,
                PublicationDate = bookModel.PublicationDate,
                ProductDimensions= bookModel.ProductDimensions,
                Title = bookModel.Title,
                //This one we have already retrieved in the db above so there's no point to use bookModel variable.
                UserId = userid,
                //we can just put our rental here as well even if it's zero at the moment because we are only displaying these properties.The user hasn't made a choice yet.
                RentalPrice = rentalPrice,
                rentalPriceOneMonth = oneMonthRental,
                rentalPriceSixMonth = sixMonthRental,
                Publisher = bookModel.Publisher,
            };

            return View(model);
        }

        //Dispose the db variable
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            
        }
    }
}