using BookCountry.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCountry.Controllers
{
    /// <summary>
    /// User accounts controller.
    /// </summary>
    public sealed class AccountsController : Controller
    {
//        private readonly UserManager<UserAccount> userManager;
//        private readonly SignInManager<UserAccount> signInManager;


        public AccountsController()
        {
            // UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager
            // this.userManager = userManager;
            // this.signInManager = signInManager;
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
        public IActionResult Login(LoginViewModel model, string returnUrl=null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if(ModelState.IsValid)
            {
//                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
//                if(result.Succeeded)
                    return RedirectToLocal(returnUrl);

//                ModelState.AddModelError("", "Invalid login attempt.");
//                return View(model);
            }
            return View(model);
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
            Url.IsLocalUrl(returnUrl) ? (IActionResult)Redirect(returnUrl) : RedirectToAction(nameof(HomeController.Index), "Home");
    }
}
