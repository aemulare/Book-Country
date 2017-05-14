using System.ComponentModel.DataAnnotations;

namespace BookCountry.Models.ViewModels
{
    /// <summary>
    /// Registration view model.
    /// </summary>
    public sealed class RegistrationViewModel : LoginViewModel
    {
        /// <summary>
        /// Password confirmation.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Password confirmation mismatch")]
        public string PasswordConfirmation { get; set; }
    }
}
