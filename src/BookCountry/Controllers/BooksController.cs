using System;
using System.Linq;
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

        public IActionResult Show(int bookId)
        {
            var book = books.List.FirstOrDefault(b => b.Id == bookId);
            return View(book);
        }
            


        [HttpPost]
        public IActionResult Create(Book book)
        { 
            book.CreatedAt = DateTime.Now;
            books.Add(book);
            return RedirectToAction(nameof(Index));
        }

    }
}
