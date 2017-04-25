using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BookCountry.Models
{
    public class LoansRepository : ILoansRepository
    {
        private readonly IConfigurationRoot configuration;

        public LoansRepository(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection Connection =>
           new MySqlConnection(configuration["Data:BookCountry:ConnectionString"]);


        // collection of authors and their books (book id)
        public IEnumerable<Loan> List
        {
            get
            {
                using (var connection = Connection)
                {
                    const string SQL = "SELECT * FROM loans as ln " +
                                        "inner join books as b on b.id = ln.bookId " +
                                        "inner join borrowers as br on br.id = ln.borrowerId";
                    connection.Open();
                    return connection.Query<Loan,Book,Borrower,Loan>(SQL,
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
}
