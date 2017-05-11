using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BookCountry.Models
{
    public class BorrowersRepository : RepositoryBase, IBorrowersRepository
    {
        private const string IDENTITY_CLAUSE = "select LAST_INSERT_ID();";
//        private const string IDENTITY_CLAUSE = "select cast(scope_identity() as int);";

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
                return connection.Query<Borrower,Address,Borrower>(SQL,
                    (borrower, address) =>
                    {
                        borrower.Address = address;
                        return borrower;
                    }).ToList();
            }
        }



        /// <summary>
        /// Creates a new borrower in DB.
        /// </summary>
        /// <param name="borrower">Borrower instance.</param>
        public void Create(Borrower borrower)
        {
            if(borrower == null)
                throw new ArgumentNullException(nameof(borrower));

            const string ADDRESS_SQL =
                "insert into addresses " +
                "(addressLine1, addressLine2, city, state, zip) " +
                "values (@AddressLine1, @AddressLine2, @City, @State, @Zip); " +
                IDENTITY_CLAUSE;
            const string BORROWER_SQL =
                "insert into borrowers " +
                "(email, firstName, lastName, dob, phone, addressId, createdAt, passwordDigest, active) " +
                "values (@Email, @FirstName, @LastName, @Dob, @Phone, @AddressId, @CreatedAt, @PasswordDigest, @Active); " +
                IDENTITY_CLAUSE;

            using(var conn = GetConnection())
            {
                if(borrower.Address != null)
                    borrower.Address.Id = conn.Query<int>(ADDRESS_SQL, borrower.Address).First();

                borrower.Id = conn.Query<int>(BORROWER_SQL,
                    new
                    {
                        borrower.Email,
                        borrower.FirstName,
                        borrower.LastName,
                        borrower.Dob,
                        borrower.Phone,
                        AddressId = borrower.Address?.Id,
                        borrower.CreatedAt,
                        borrower.PasswordDigest,
                        borrower.Active
                    }).First();
            }
        }



        /// <summary>
        /// Updates a borrower in DB.
        /// </summary>
        /// <param name="borrower">Borrower instance.</param>
        public void Update(Borrower borrower)
        {
        }



        public void Delete(Borrower borrower)
        {
        }
    }
}
