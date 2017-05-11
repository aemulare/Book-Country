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
        /// Displays a form to edit a borrower
        /// </summary>
        public IActionResult Edit(string borrowerId)
        {
            var borrower = borrowers.GetById(Convert.ToInt32(borrowerId));
            return View("Profile", borrower);
        }



        /// <summary>
        /// POST update action.
        /// Updates a borrower.
        /// </summary>
        [HttpPost]
        public IActionResult Update(Borrower borrower, int borrowerId)
        {
            if(ModelState.IsValid)
            {
                var actualBorrower = borrowers.GetById(borrowerId);
                actualBorrower.FirstName = borrower.FirstName;
                actualBorrower.LastName = borrower.LastName;
                actualBorrower.Dob = DateTime.Now - TimeSpan.FromDays(3560);
                actualBorrower.Address = borrower.Address;
                borrowers.Update(actualBorrower);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Edit), borrower);
        }
    }
}
