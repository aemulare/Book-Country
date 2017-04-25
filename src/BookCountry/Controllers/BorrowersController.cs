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

        // GET: /<controller>/
        public IActionResult Index() => View(borrowers.GetAll());

        // new borrower form
        public IActionResult New() => View();

    }
}
