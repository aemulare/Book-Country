using System;
using System.Linq;
using BookCountry.Models;
using BookCountry.Tools;
using BookCountry.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BookCountry.Controllers
{
    public class BooksController : Controller
    {
        // field
        private IBooksRepository books;

        // constructor
        public BooksController(IBooksRepository repo)
        {
            this.books = repo;
        }

        // GET: /<controller>/
        public IActionResult Index() => View(books.GetAll());

        public IActionResult Tile() => View(books.GetAll());


        // New book form
        public IActionResult New()
        {
            var book = new BookViewModel
            {
                Languages = from lang in books.Languages
                    select new SelectListItem {Text = lang.Name, Value = lang.Id.ToString()},
                Formats = from f in books.Formats
                    select new SelectListItem {Text = f.Name, Value = f.Id.ToString()},
                Publishers = from pub in books.Publishers
                    select new SelectListItem {Text = pub.Name, Value = pub.Id.ToString()}
            };

            return View(book);
        } 


        // Book details view
        public IActionResult Show(int bookId)
        {
            var book = books.GetAll().FirstOrDefault(b => b.Id == bookId);
            return View(book);
        }
            

        // Adds new book
        [HttpPost]
        public IActionResult Create(BookViewModel viewModel)
        {
            if (IsbnParser.IsValid(viewModel.Book.Isbn))
                if (ModelState.IsValid)
                {
                    var authors = viewModel.Authors.Split(',');
                    var ordinal = 0;
                    foreach (var a in authors)
                    {
                    
                        var names = a.TrimStart().Split(' ');
                        var firstName = names.FirstOrDefault();
                        var lastName = names.Last();
                        var author = books.FindAuthors(firstName, lastName).FirstOrDefault()
                            ?? new Author { FirstName = firstName, LastName = lastName };
                        viewModel.Book.BooksAuthors.Add(new BookAuthor { Author = author, AuthorOrdinal = ++ordinal });
                    }
                    viewModel.Book.Cover = "https://unsplash.it/250/350/?random";
                    viewModel.Book.CreatedAt = DateTime.Now;
                    books.Save(viewModel.Book);
                    TempData["message"] = "The new book has been saved.";
                    return RedirectToAction(nameof(Index));
                }

            ModelState.AddModelError("", "The ISBN is not valid.");
            TempData["error"] = "Error. The ISBN is not valid. Please try again.";
            return RedirectToAction(nameof(New), viewModel);
            //return View(nameof(New), viewModel);
        }
    }
}
