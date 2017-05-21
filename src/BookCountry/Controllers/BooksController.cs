using System;
using System.Linq;
using BookCountry.Models;
using BookCountry.Models.ViewModels;
using BookCountry.Tools;
using BookCountry.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookCountry.Controllers
{
    /// <summary>
    /// Books controller.
    /// </summary>
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBooksRepository books;
        private readonly IBorrowersRepository borrowers;
        private readonly ILoansRepository loans;


        /// <summary>
        /// Constructors.
        /// </summary>
        public BooksController(IBooksRepository repo, IBorrowersRepository borrowers, ILoansRepository loans)
        {
            this.books = repo;
            this.borrowers = borrowers;
            this.loans = loans;
        }



        [AllowAnonymous]
        public IActionResult Tile() => View(new BookTilesViewModel(books.GetAll()));



        /// <summary>
        /// POST search action.
        /// Performs search books operation.
        /// </summary>
        /// <param name="bookTiles">Book tiles view model.</param>
        [AllowAnonymous]
        public IActionResult Search(BookTilesViewModel bookTiles)
        {
            var searchResult = !string.IsNullOrWhiteSpace(bookTiles.SearchTemplate)
                ? books.Search(bookTiles.SearchTemplate) : books.GetAll();
            return View(nameof(BooksController.Tile), new BookTilesViewModel(searchResult));
        }



        // New book form
        public IActionResult New()
        {
            if(!borrowers.CurrentUser.IsLibrarian)
                return Redirect("~/403.html");

            var book = new BookViewModel
            {
                Languages = from lang in books.Languages
                    select new SelectListItem { Text = lang.Name, Value = lang.Id.ToString() },
                Formats = from f in books.Formats
                    select new SelectListItem { Text = f.Name, Value = f.Id.ToString() },
                Publishers = from pub in books.Publishers
                    select new SelectListItem { Text = pub.Name, Value = pub.Id.ToString() }
            };
            return View(book);
        }



        // Book details view
        [AllowAnonymous]
        public IActionResult Show(int bookId)
        {
            var book = books.GetAll().FirstOrDefault(b => b.Id == bookId);
            var reserved = loans.CountReserved(book);
            book.AvailableCount = (book.Quantity ?? 0) - reserved;
            return View(book);
        }



        // Adds new book
        [HttpPost]
        public IActionResult Create(BookViewModel viewModel)
        {
            if(!borrowers.CurrentUser.IsLibrarian)
                return Redirect("~/403.html");

            if(IsbnParser.IsValid(viewModel.Book.Isbn))
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
                    return RedirectToAction(nameof(Tile));
                }

            ModelState.AddModelError("", "The ISBN is not valid.");
            TempData["error"] = "Error. The ISBN is not valid. Please try again.";
            return RedirectToAction(nameof(New), viewModel);
        }
    }
}
