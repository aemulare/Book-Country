using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BookCountry.Models
{
    public class BorrowersRepository : IBorrowersRepository
    {
        private readonly IConfigurationRoot configuration;

        public BorrowersRepository(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection Connection =>
           new MySqlConnection(configuration["Data:BookCountry:ConnectionString"]);

        // collection of books
        public IEnumerable<Borrower> List
        {
            get
            {
                using (var connection = Connection)
                {
                    const string SQL = "SELECT * FROM borrowers as br " +
                                 "inner join addresses as addr ON addr.id = br.addressId";

                    connection.Open();
                    return connection.Query<Borrower,Address,Borrower>(SQL,
                        (borrower, address) =>
                        {
                            borrower.Address = address;
                            return borrower;
                        }).ToList();
                }
            }
        }


        // collection of authors and their books (book id)
        public IEnumerable<Address> Addresses
        {
            get
            {
                using (var connection = Connection)
                {
                    string q = "SELECT id, " +
                               "addressLine1, " +
                               "addressLine2, " +
                               "city, " +
                               "state, " +
                               "zip " +
                               "FROM addresses";
                    connection.Open();
                    return connection.Query<Address>(q);
                }
            }
        }
    }
}
