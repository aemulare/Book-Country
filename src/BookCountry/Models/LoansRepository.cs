using System.Collections.Generic;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BookCountry.Models
{
    public class LoansRepository : RepositoryBase, ILoansRepository
    {
        // constructor
        public LoansRepository(IConfigurationRoot configuration) : base(configuration) { }

        // collection of borrowers, books and dates (loan history)
        public IEnumerable<Loan> GetAll()
        {
            using (var connection = GetConnection())
            {
                const string SQL = "SELECT * FROM loans as ln " +
                                    "inner join books as b on b.id = ln.bookId " +
                                    "inner join borrowers as br on br.id = ln.borrowerId";
                connection.Open();
                return connection.Query<Loan, Book, Borrower, Loan>(SQL,
                    (loan, book, borrower) =>
                    {
                        loan.Book = book;
                        loan.Borrower = borrower;
                        return loan;
                    });
            }
        }
    }
}
