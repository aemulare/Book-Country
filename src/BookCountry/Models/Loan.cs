using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCountry.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int BorrowerId { get; set; }
        public int BookId { get; set; }

        public DateTime ReservedAt { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime ReturnedOn { get; set; }
    }
}
