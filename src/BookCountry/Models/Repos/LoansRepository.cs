using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BookCountry.Models
{
    public class LoansRepository : RepositoryBase, ILoansRepository
    {
        private const string LOANS_SQL = "SELECT * FROM loans as ln " +
            "INNER join books as b ON b.id = ln.bookId " +
            "INNER join borrowers as br ON br.id = ln.borrowerId";


        // constructor
        public LoansRepository(IConfigurationRoot configuration) : base(configuration)
        {
        }



        /// <summary>
        /// Gets all loans.
        /// </summary>
        /// <returns>A collection of all loans.</returns>
        public IEnumerable<Loan> GetAll() => QueryLoans(LOANS_SQL);

        /// <summary>
        /// Gets a collection of all reservations for the specified borrower.
        /// </summary>
        /// <param name="borrowerId">Borrower unique ID.</param>
        /// <returns>A collection of reserved book.</returns>
        public IEnumerable<Loan> GetReservations(int borrowerId) =>
            QueryLoans(LOANS_SQL + " WHERE ln.borrowerId = @BorrowerId;", new { BorrowerId = borrowerId });



        /// <summary>
        /// Determines the number of reservations for the specified book.
        /// </summary>
        /// <param name="book">Book instance.</param>
        /// <param name="borrower">Borrower instance.</param>
        /// <returns>The number of reservation.</returns>
        public int CountReserved(Book book, Borrower borrower)
        {
            const string COUNT_SQL = "SELECT COUNT(*) FROM loans WHERE bookId = @BookId";
            var sql = borrower != null ? COUNT_SQL + " and borrowerId = @BorrowerId" : COUNT_SQL;
            using(var connection = GetConnection())
            {
                connection.Open();
                return connection.Query<int>(sql,
                    new
                    {
                        BookId = book.Id,
                        BorrowerId = borrower?.Id
                    }).First();
            }
        }



        /// <summary>
        /// Reserves a book.
        /// Adds a new record in loans data table.
        /// </summary>
        public void Add(Loan loan)
        {
            const string SQL = "INSERT INTO loans (borrowerId, bookId, reservedAt) " +
                "VALUES (@BorrowerId, @BookId, @ReservedAt); " +
                IDENTITY_CLAUSE;

            using(var conn = GetConnection())
            {
                conn.Open();
                using(var trans = conn.BeginTransaction())
                {
                    loan.Id = conn.Query<int>(SQL,
                        new
                        {
                            BorrowerId = loan.Borrower.Id,
                            BookId = loan.Book.Id,
                            loan.ReservedAt
                        },
                        trans).First();
                    trans.Commit();
                }
            }
        }



        /// <summary>
        /// Deletes a record from loans data table.
        /// </summary>
        /// <param name="loanId">Loan unique ID.</param>
        public void Delete(int loanId)
        {
            const string SQL = "DELETE from loans WHERE id = @LoanId";
            using(var conn = GetConnection())
            {
                conn.Open();
                using(var trans = conn.BeginTransaction())
                {
                    conn.Execute(SQL, new { LoanId = loanId }, trans);
                    trans.Commit();
                }
            }
        }



        public IEnumerable<Loan> QueryLoans(string sql, object param=null)
        {
            using(var connection = GetConnection())
            {
                connection.Open();
                return connection.Query<Loan,Book,Borrower,Loan>(sql,
                    (loan, book, borrower) =>
                    {
                        loan.Book = book;
                        loan.Borrower = borrower;
                        return loan;
                    }, param);
            }
        }
    }
}
