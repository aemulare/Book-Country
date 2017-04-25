using BookCountry.Models;
using Microsoft.AspNetCore.Mvc;


namespace BookCountry.Controllers
{
    public class LoansController : Controller
    {
        // field
        private readonly ILoansRepository loans;

        // constructor
        public LoansController(ILoansRepository repo)
        {
            this.loans = repo;
        }

        // GET: /<controller>/
        public IActionResult Index() => View(loans.GetAll());
    }
}
