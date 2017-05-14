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
                const string SQL = "SELECT * FROM borrowers as br " +
                    "LEFT JOIN addresses as addr ON addr.id = br.addressId";
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
            if (borrower == null)
                throw new ArgumentNullException(nameof(borrower));

            const string ADDRESS_SQL =
                "insert into addresses " +
                "(addressLine1, addressLine2, city, state, zip) " +
                "values (@AddressLine1, @AddressLine2, @City, @State, @Zip); " +
                IDENTITY_CLAUSE;
            const string BORROWER_SQL =
                "update borrowers " +
                "set firstName = @FirstName, lastName = @LastName, dob = @Dob, phone = @Phone, " +
                "addressId = @AddressId " +
                "where id = @Id";

            using (var conn = GetConnection())
            {
                if (borrower.Address != null)
                    borrower.Address.Id = conn.Query<int>(ADDRESS_SQL, borrower.Address).First();

                conn.Execute(BORROWER_SQL,
                    new
                    {
                        borrower.Id,
                        borrower.FirstName,
                        borrower.LastName,
                        borrower.Dob,
                        borrower.Phone,
                        AddressId = borrower.Address?.Id,
                    });
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
                const string SQL = "SELECT * FROM borrowers as b " +
                    "inner join addresses as a on a.id = b.addressId " +
                    "where b.email = @Email;";
                connection.Open();
                return connection.Query<Borrower,Address,Borrower>(SQL,
                    (borrower,address) =>
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
                const string SQL = "SELECT * FROM borrowers as b " +
                    "inner join addresses as a on a.id = b.addressId " +
                    "where b.id = @BorrowerId;";
                connection.Open();
                return connection.Query<Borrower,Address,Borrower>(SQL,
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
