using System;
using BookCountry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace BookCountry.Controllers
{
    [Authorize]
    public class LoansController : Controller
    {
        // field
        private readonly ILoansRepository loans;
        private readonly IBorrowersRepository borrowers;
        private readonly IBooksRepository books;
        private readonly IToastNotification toasts;

        // constructor
        public LoansController(ILoansRepository loans, IBorrowersRepository borrowers,
            IBooksRepository books, IToastNotification toasts)
        {
            this.loans = loans;
            this.borrowers = borrowers;
            this.books = books;
            this.toasts = toasts;
        }



        public IActionResult Index()
        {
            if(!borrowers.CurrentUser.IsLibrarian)
                return Redirect("~/403.html");

            return View(loans.GetAll());
        }



        /// <summary>
        /// GET reserve.
        /// Reserves a book for the current borrower.
        /// </summary>
        /// <param name="bookId">Book unique ID.</param>
        public IActionResult Reserve(int bookId)
        {
            var book = books.GetById(bookId);
            var loan = new Loan
            {
                Borrower = borrowers.CurrentUser,
                Book = book,
                ReservedAt = DateTime.Now,
            };
            loans.Add(loan);
            this.toasts.AddToastMessage("Book reserved", $"'{book.Title}' has been reserved",
                ToastEnums.ToastType.Success);
            return RedirectToAction(nameof(BooksController.Tile), "Books");
        }



        /// <summary>
        /// GET reservations action.
        /// </summary>
        public IActionResult Reservations()
        {
            var borrower = borrowers.CurrentUser;
            var reservations = loans.GetReservations(borrower.Id);
            return View(reservations);
        }



        public IActionResult Cancel(int loanId)
        {
            loans.Delete(loanId);
            this.toasts.AddToastMessage("Cancel Reservation", "The book reservation has been canceled",
                ToastEnums.ToastType.Success);
            return RedirectToAction(nameof(LoansController.Reservations));
        }



        public IActionResult CancelAll()
        {
            var borrower = borrowers.CurrentUser;
            var reservations = loans.GetReservations(borrower.Id);
            foreach(var loan in reservations)
                loans.Delete(loan.Id);
            this.toasts.AddToastMessage("Cancel Reservation", "All books reservation has been canceled",
                ToastEnums.ToastType.Success);
            return RedirectToAction(nameof(LoansController.Reservations));
        }
    }
}
