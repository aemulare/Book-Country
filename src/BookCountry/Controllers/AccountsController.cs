using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BookCountry.Helpers;
using BookCountry.Models;
using BookCountry.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace BookCountry.Controllers
{
    /// <summary>
    /// User accounts controller.
    /// </summary>
    [Authorize]
    public sealed class AccountsController : Controller
    {
        private readonly IBorrowersRepository borrowers;
        private readonly IToastNotification toasts;


        public AccountsController(IBorrowersRepository borrowers, IToastNotification toasts)
        {
            this.borrowers = borrowers;
            this.toasts = toasts;
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
                Borrower borrower;
                if(IsAuthenticated(user, out borrower))
                {
                    await LoginImpl(user.Email);
                    this.toasts.AddToastMessage("Welcome", $"Hello {borrower.GreetingName}! Welcome to Book Country",
                        ToastEnums.ToastType.Success);
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError("", "Invalid login attempt.");
                this.toasts.AddToastMessage("Access denied", "Invalid user email or password", ToastEnums.ToastType.Error);
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
                if(borrowers.GetByEmail(user.Email) != null)
                {
                    var errorMessage = $"The email '{user.Email}' already registered";
                    ModelState.AddModelError("", errorMessage);
                    this.toasts.AddToastMessage("Email exists", errorMessage, ToastEnums.ToastType.Warning);
                    return View(user);
                }
                var borrower = new Borrower
                {
                    Email = user.Email,
                    PasswordDigest = EncriptionHelper.Sha256Hash(user.Password),
                    CreatedAt = DateTime.Now,
                    Active = true
                };

                borrowers.Create(borrower);
                await LoginImpl(user.Email);
                this.toasts.AddToastMessage("Welcome!", $"Welcome '{user.Email}'! Please enter your personal info.",
                    ToastEnums.ToastType.Success);
                return RedirectToAction(nameof(BorrowersController.Edit), "Borrowers", new { borrowerId = borrower.Id });
            }
            return View(user);
        }



        private IActionResult RedirectToLocal(string returnUrl) =>
            Url.IsLocalUrl(returnUrl) ? (IActionResult)Redirect(returnUrl) : RedirectToAction(nameof(BooksController.Tile), "Books");



        private bool IsAuthenticated(LoginViewModel user, out Borrower borrower)
        {
            borrower = borrowers.GetByEmail(user.Email);
            return borrower != null && borrower.PasswordDigest == EncriptionHelper.Sha256Hash(user.Password);
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
