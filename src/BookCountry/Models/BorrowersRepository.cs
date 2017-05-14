using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BookCountry.Models
{
    /// <summary>
    /// Borrowers repository.
    /// </summary>
    public class BorrowersRepository : RepositoryBase, IBorrowersRepository
    {
        private const string BORROWERS_SQL = "SELECT * FROM borrowers as b " +
            "LEFT JOIN addresses as a ON a.id = b.addressId";

        private readonly IHttpContextAccessor http;


        /// <summary>
        /// Constructor.
        /// </summary>
        public BorrowersRepository(IConfigurationRoot configuration, IHttpContextAccessor context)
            : base(configuration)
        {
            this.http = context;
        }



        /// <summary>
        /// Returns a current authenticated user (borrower).
        /// </summary>
        public Borrower CurrentUser
        {
            get
            {
                var user = http.HttpContext.User;
                if(!user.Identity.IsAuthenticated)
                    return null;
                var emailClaim = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
                var email = emailClaim?.Value;
                return email != null ? GetByEmail(email) : null;
            }
        }



        /// <summary>
        /// Gets all borrowers from DB.
        /// </summary>
        /// <returns>A collection of all borrowers.</returns>
        public IEnumerable<Borrower> GetAll()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                return connection.Query<Borrower,Address,Borrower>(BORROWERS_SQL,
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

            const string INSERT_BORROWER_SQL =
                "INSERT INTO borrowers " +
                "(email, firstName, lastName, dob, phone, addressId, createdAt, passwordDigest, active) " +
                "values (@Email, @FirstName, @LastName, @Dob, @Phone, @AddressId, @CreatedAt, @PasswordDigest, @Active); " +
                IDENTITY_CLAUSE;

            using(var conn = GetConnection())
            {
                conn.Open();
                using(var trans = conn.BeginTransaction())
                {
                    borrower.Id = conn.Query<int>(INSERT_BORROWER_SQL,
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
                        }, trans).First();
                    trans.Commit();
                }
            }
        }



        /// <summary>
        /// Updates a borrower in DB.
        /// </summary>
        /// <param name="borrower">Borrower instance.</param>
        public void Update(Borrower borrower)
        {
            if (borrower == null)
                throw new ArgumentNullException(nameof(borrower));

            const string INSERT_ADDRESS_SQL =
                "INSERT INTO addresses " +
                "(addressLine1, addressLine2, city, state, zip) " +
                "values (@AddressLine1, @AddressLine2, @City, @State, @Zip); " +
                IDENTITY_CLAUSE;
            const string UPDATE_ADDRESS_SQL =
                "UPDATE addresses " +
                "SET addressLine1 = @AddressLine1, addressLine2 = @AddressLine2, city = @City, " +
                "state = @State, zip = @Zip";
            const string UPDATE_BORROWER_SQL =
                "UPDATE borrowers " +
                "SET firstName = @FirstName, lastName = @LastName, dob = @Dob, phone = @Phone, " +
                "addressId = @AddressId " +
                "WHERE id = @Id";

            using(var conn = GetConnection())
            {
                conn.Open();
                using(var trans = conn.BeginTransaction())
                {
                    int? borrowerAddressId = null;
                    if(borrower.Address != null && borrower.Address.Id != 0)
                        conn.Execute(UPDATE_ADDRESS_SQL, borrower.Address, trans);
                    else
                        borrowerAddressId = conn.Query<int>(INSERT_ADDRESS_SQL, borrower.Address, trans).First();
                    conn.Execute(UPDATE_BORROWER_SQL,
                        new
                        {
                            borrower.Id,
                            borrower.FirstName,
                            borrower.LastName,
                            borrower.Dob,
                            borrower.Phone,
                            AddressId = borrowerAddressId ?? borrower.Address.Id
                        }, trans);
                    trans.Commit();
                }
            }
        }



        /// <summary>
        /// Gets a borrower by email.
        /// </summary>
        /// <param name="email">Email address.</param>
        /// <returns>A borrower instance, if any.</returns>
        public Borrower GetByEmail(string email)
        {
            using(var connection = GetConnection())
            {
                connection.Open();
                return connection.Query<Borrower, Address, Borrower>(BORROWERS_SQL + " WHERE b.email = @Email;",
                    (borrower, address) =>
                    {
                        borrower.Address = address;
                        return borrower;
                    },
                    new { Email = email }).FirstOrDefault();
            }
        }



        /// <summary>
        /// Gets a borrower by ID.
        /// </summary>
        /// <param name="borrowerId">Borrower ID.</param>
        /// <returns>A borrower instance, if any.</returns>
        public Borrower GetById(int borrowerId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                return connection.Query<Borrower,Address,Borrower>(BORROWERS_SQL + " WHERE b.id = @BorrowerId;",
                    (borrower, address) =>
                    {
                        borrower.Address = address;
                        return borrower;
                    },
                    new { BorrowerId = borrowerId }).FirstOrDefault();
            }
        }
    }
}
