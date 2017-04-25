using System;
using System.Linq;
using BookCountry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


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
        public IActionResult Index() => View(books.GetAll());

        public IActionResult Tile() => View(books.GetAll());

        public IActionResult New()
        {
           
           ViewBag.Languages = books.Languages.Select(l => new SelectListItem { Text = l.Name, Value = l.Id.ToString() });
           ViewBag.Formats = books.Formats.Select(f => new SelectListItem { Text = f.Name, Value = f.Id.ToString() });
           var book = new Book();
           return View(book);
        } 

        public IActionResult Show(int bookId)
        {
            var book = books.GetAll().FirstOrDefault(b => b.Id == bookId);
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
