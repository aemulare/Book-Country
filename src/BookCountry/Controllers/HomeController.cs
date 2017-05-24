using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCountry.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult About()
        {
            ViewData["Message"] = "Welcome to Book Country!";

            return View();
        }



        [AllowAnonymous]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Please contact us with information provides below.";
            return View();
        }
    }
}
