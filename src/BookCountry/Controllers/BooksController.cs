using System;
using BookCountry.Models;
using Microsoft.AspNetCore.Mvc;


namespace BookCountry.Controllers
{
    public class BooksController : Controller
    {
        private IBooksRepository books;

        public BooksController(IBooksRepository repo)
        {
            this.books = repo;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(books.List);
        }



        public IActionResult New()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Book book)
        {
            // hard-coded test book
            //var book = new Book
            //{
            //    Title = "NEW BOOK",
            //    PublishedOn = DateTime.Today,
            //    PublisherId = 6,
            //    LanguageId = 139,
            //    FormatId = 1,
            //    Isbn = "1234567892345",
            //    DeweyCode = "005.23",
            //    Quantity = 1,
            //    CreatedAt = DateTime.Now
            //};
            book.CreatedAt = DateTime.Now;
            books.Add(book);
            return RedirectToAction(nameof(Index));
        }

    }
}
