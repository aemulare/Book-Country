using System.ComponentModel.DataAnnotations;

namespace BookCountry.Models.ViewModels
{
    /// <summary>
    /// Registration view model.
    /// </summary>
    public sealed class RegistrationViewModel
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
        /// Password confirmation.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
    }
}
