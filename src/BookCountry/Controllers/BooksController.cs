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
        public IActionResult Index() => View(books.List);

        public IActionResult Tile() => View(books.List);

        public IActionResult New() => View();

        public IActionResult Show() => View();


        [HttpPost]
        public IActionResult Create(Book book)
        { 
            book.CreatedAt = DateTime.Now;
            books.Add(book);
            return RedirectToAction(nameof(Index));
        }

    }
}
