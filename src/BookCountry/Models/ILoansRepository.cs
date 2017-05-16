using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface ILoansRepository
    {
        /// <summary>
        /// Gets all loans.
        /// </summary>
        /// <returns>A collection of all loans.</returns>
        IEnumerable<Loan> GetAll();

        /// <summary>
        /// Gets a collection of all reservations for the specified borrower.
        /// </summary>
        /// <param name="borrowerId">Borrower unique ID.</param>
        /// <returns>A collection of reserved book.</returns>
        IEnumerable<Loan> GetReservations(int borrowerId);

        /// <summary>
        /// Adds a new record in loans data table.
        /// </summary>
        void Add(Loan loan);

        /// <summary>
        /// Deletes a record from loans data table.
        /// </summary>
        /// <param name="loanId">Loan unique ID.</param>
        void Delete(int loanId);
    }
}
