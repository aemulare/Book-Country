using System;
using System.ComponentModel.DataAnnotations;

namespace BookCountry.Models
{
    /// <summary>
    /// Borrower business domain model.
    /// Represents a regular user in the application.
    /// Can loan books in a library.
    /// </summary>
    public sealed  class Borrower : PersistentEntity
    {
        /// <summary>
        /// Borrower email (used as a user ID for authentiation purposes).
        /// </summary>
        [Required(ErrorMessage="Email address can't be blank")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Borrower first name.
        /// </summary>
        [Required(ErrorMessage="Borrower first name can't be blank")]
        public string FirstName { get; set; }

        /// <summary>
        /// Borrower last name.
        /// </summary>
        [Required(ErrorMessage="Borrower last name can't be blank")]
        public string LastName { get; set; }

        /// <summary>
        /// Borrower full name.
        /// </summary>
        public string FullName => FirstName + " " + LastName;

        /// <summary>
        /// Borrower greeting name.
        /// </summary>
        public string GreetingName => string.IsNullOrWhiteSpace(FirstName) ? Email : FirstName;

        /// <summary>
        /// Borrower date of birth.
        /// </summary>
        [Required(ErrorMessage="Date of birth can't be blank")]
        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }

        /// <summary>
        /// Borrower phone number.
        /// </summary>
        [Required(ErrorMessage="Phone number can't be blank")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        /// <summary>
        /// Borrower address.
        /// </summary>
        
        public Address Address { get; set; }

        /// <summary>
        /// Password digest.
        /// </summary>
        public string PasswordDigest { get; set; }

        /// <summary>
        /// Borrower activation token.
        /// </summary>
        public string ActivationToken { get; set; }

        /// <summary>
        /// Determines whether a borrower is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Determines whether a borrower is a librarian.
        /// </summary>
        public bool IsLibrarian { get; set; }

        /// <summary>
        /// A timestamp whether the given borrower was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
