using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCountry.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }



        [AllowAnonymous]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }
    }
}
