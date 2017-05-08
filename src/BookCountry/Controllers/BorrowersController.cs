using System;
using BookCountry.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookCountry.Controllers
{
    public class BorrowersController : Controller
    {
        // field
        private readonly IBorrowersRepository borrowers;

        // constructor
        public BorrowersController(IBorrowersRepository repo)
        {
            this.borrowers = repo;
        }



        /// <summary>
        /// GET index action.
        /// Displays a list of all borrowers.
        /// </summary>
        public IActionResult Index() => View(borrowers.GetAll());

        /// <summary>
        /// GET new action.
        /// Displays a form to create a new borrower
        /// </summary>
        public IActionResult New() => View(new Borrower());

        /// <summary>
        /// POST create action.
        /// Creates a new borrower.
        /// </summary>
        /// <param name="borrower">Borrower instance.</param>
        [HttpPost]
        public IActionResult Create(Borrower borrower)
        {
            if(ModelState.IsValid)
            {
                borrower.Dob = DateTime.Now - TimeSpan.FromDays(3560);
                borrower.CreatedAt = DateTime.Now;
                borrower.Active = true;
                borrower.PasswordDigest = "KVA";
                borrowers.Create(borrower);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(New), borrower);
        }
    }
}
