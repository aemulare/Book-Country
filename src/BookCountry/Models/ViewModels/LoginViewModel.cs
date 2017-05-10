using System.ComponentModel.DataAnnotations;

namespace BookCountry.Models.ViewModels
{
    /// <summary>
    /// Login view model.
    /// </summary>
    public sealed class LoginViewModel
    {
        /// <summary>
        /// Email address used as user ID.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Determines whether a login/password should be remembered.
        /// </summary>
        [Display(Name="Remember me?")]
        public bool RememberMe { get; set; }
    }
}
