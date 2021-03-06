﻿using System;

namespace BookCountry.Models
{
    public class Loan : PersistentEntity
    {
        public Borrower Borrower {get; set; }
        public Book Book { get; set; }

        public DateTime ReservedAt { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? ReturnedOn { get; set; }
    }
}
