using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BookCountry.Models;
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
        private readonly IBorrowersRepository borrowers;


        public AccountsController(IBorrowersRepository borrowers)
        {
            this.borrowers = borrowers;
        }



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
                if(IsAuthenticated(user))
                {
                    await LoginImpl(user.Email);
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
        public IActionResult Register() => View();

        /// <summary>
        /// POST register action.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationViewModel user)
        {
            if(ModelState.IsValid)
            {
                if (borrowers.GetByEmail(user.Email) != null)
                {
                    ModelState.AddModelError("", "This email is already exists.");
                    TempData["error"] = "The user with this email is already registered.";
                    return View(user);
                }
                var borrower = new Borrower
                {
                    Email = user.Email,
                    PasswordDigest = user.Hash,
                    CreatedAt = DateTime.Now,
                    Active = true
                };
                
                borrowers.Create(borrower);
                await LoginImpl(user.Email);
                return RedirectToAction(nameof(BorrowersController.Edit), "Borrowers", new { borrowerId = borrower.Id.ToString() });
            }
            return View(user);
        }



        private IActionResult RedirectToLocal(string returnUrl) =>
            Url.IsLocalUrl(returnUrl) ? (IActionResult)Redirect(returnUrl) : RedirectToAction(nameof(BooksController.Tile), "Books");



        private bool IsAuthenticated(LoginViewModel user)
        {
            var borrower = borrowers.GetByEmail(user.Email);
            return borrower != null && borrower.PasswordDigest == user.Hash;
        }



        private async Task LoginImpl(string email)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, email) };
            var props = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), props);
        }
    }
}
