using BookCountry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCountry.Controllers
{
    [Authorize]
    public class LoansController : Controller
    {
        // field
        private readonly ILoansRepository loans;
        private readonly IBorrowersRepository borrowers;

        // constructor
        public LoansController(ILoansRepository loans, IBorrowersRepository borrowers)
        {
            this.loans = loans;
            this.borrowers = borrowers;
        }



        public IActionResult Index()
        {
            if(!borrowers.CurrentUser.IsLibrarian)
                return Redirect("~/403.html");

            return View(loans.GetAll());
        }
    }
}
