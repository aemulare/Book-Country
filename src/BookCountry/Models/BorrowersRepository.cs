using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BookCountry.Models
{
    public class BorrowersRepository : RepositoryBase, IBorrowersRepository
    {
        // constructor
        public BorrowersRepository(IConfigurationRoot configuration) : base(configuration) { }

        // collection of books
        public IEnumerable<Borrower> GetAll()
        {
            using (var connection = GetConnection())
            {
                const string SQL = "SELECT * FROM borrowers as br " +
                                   "INNER JOIN addresses as addr ON addr.id = br.addressId";

                connection.Open();
                return connection.Query<Borrower, Address, Borrower>(SQL,
                    (borrower, address) =>
                    {
                        borrower.Address = address;
                        return borrower;
                    }).ToList();
            }
        }
    }
}
