using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCountry.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookCountry.Controllers
{
    public class LoansController : Controller
    {
        private ILoansRepository loans;

        public LoansController(ILoansRepository repo)
        {
            this.loans = repo;
        }

        // GET: /<controller>/
        public IActionResult Index() => View(loans.List);
    }
}
