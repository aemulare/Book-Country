using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BookCountry.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BookCountry.Controllers
{
    /// <summary>
    /// User accounts controller.
    /// </summary>
    public sealed class AccountsController : Controller
    {
        /// <summary>
        /// GET Login action.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl=null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }



        /// <summary>
        /// POST Login action.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel user, string returnUrl=null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if(ModelState.IsValid)
            {
                if(user.Email == "gwen@hvost.com" && user.Password == "hvost")
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email) };
                    var props = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity), props);
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError("", "Invalid login attempt.");
                TempData["error"] = "Invalid user email or password.";
                return View(user);
            }
            return View(user);
        }



        /// <summary>
        /// GET Logout action.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToLocal(null);
        }



        /// <summary>
        /// GET register action.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }



        private IActionResult RedirectToLocal(string returnUrl) =>
            Url.IsLocalUrl(returnUrl) ? (IActionResult)Redirect(returnUrl) : RedirectToAction(nameof(BooksController.Tile), "Books");
    }
}
