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
                    string q = "SELECT br.id, " +
                               "br.email, " +
                               "br.firstName, " +
                               "br.lastName, " +
                               "br.dob, " +
                               "br.phone, " +
                               "br.addressId, " +
                               "br.isLibrarian, " +
                               "br.createdAt " +
                               "FROM borrowers br";

                    connection.Open();
                    var borrowers = connection.Query<Borrower>(q).ToList();
                    var addresses = Addresses.ToList();
                    foreach (var br in borrowers)
                        br.Address = addresses.FirstOrDefault(a => a.Id == br.AddressId);
                    return borrowers;
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
