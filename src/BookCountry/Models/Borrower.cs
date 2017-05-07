using System;

namespace BookCountry.Models
{
    /// <summary>
    /// Borrower business domain model.
    /// Represents a regular user in the application.
    /// Can loan books in a library.
    /// </summary>
    public class Borrower
    {
        /// <summary>
        /// Borrower ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Borrower email (used as a user ID for authentiation purposes).
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Borrower first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Borrower last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Borrower full name.
        /// </summary>
        public string FullName => FirstName + " " + LastName;

        /// <summary>
        /// Borrower date of birth.
        /// </summary>
        public DateTime Dob { get; set; }

        /// <summary>
        /// Borrower phone number.
        /// </summary>
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
