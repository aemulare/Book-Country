using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCountry.Models;
using Microsoft.AspNetCore.Mvc;


namespace BookCountry.Controllers
{
    public class BooksController : Controller
    {
        private IBooksRepository repo;

        public BooksController(IBooksRepository repo)
        {
            this.repo = repo;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(repo.Books);
        }


    }
}
