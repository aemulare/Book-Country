using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCountry.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;

        public DateTime Dob { get; set; }

        public string Phone { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }

        public bool IsLibrarian { get; set; }
        public DateTime CreatedAt { get; set; }

        public string PasswordDigest { get; set; }
        public string ActivationToken { get; set; }
        public bool Active { get; set; }
    }
}
