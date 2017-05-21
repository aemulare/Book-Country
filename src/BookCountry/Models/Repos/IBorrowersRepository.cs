using System.Collections.Generic;

namespace BookCountry.Models
{
    /// <summary>
    /// Borrowers repository interface.
    /// </summary>
    public interface IBorrowersRepository
    {
        /// <summary>
        /// Returns a current authenticated user (borrower).
        /// </summary>
        Borrower CurrentUser { get; }

        /// <summary>
        /// Gets all borrowers from DB.
        /// </summary>
        /// <returns>A collection of all borrowers.</returns>
        IEnumerable<Borrower> GetAll();

        /// <summary>
        /// Gets a borrower by email.
        /// </summary>
        /// <param name="email">Email address.</param>
        /// <returns>A borrower instance, if any.</returns>
        Borrower GetByEmail(string email);

        /// <summary>
        /// Gets a borrower by ID.
        /// </summary>
        /// <param name="borrowerId">Borrower ID.</param>
        /// <returns>A borrower instance, if any.</returns>
        Borrower GetById(int borrowerId);

        /// <summary>
        /// Creates a new borrower in DB.
        /// </summary>
        /// <param name="borrower">Borrower instance.</param>
        void Create(Borrower borrower);

        /// <summary>
        /// Updates a borrower properties in DB.
        /// </summary>
        /// <param name="borrower">Borrower instance.</param>
        void Update(Borrower borrower);
    }
}
