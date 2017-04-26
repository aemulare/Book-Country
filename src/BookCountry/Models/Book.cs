using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace BookCountry.Models
{
    public class Book
    { 
        public int Id { get; set; }
        public string Title { get; set; }

        public List<BookAuthor> BooksAuthors { get; set; }

        public string Authors => (from a in BooksAuthors select a.FullName).ToArray().Join(", ");

        public string Edition { get; set; }
        public DateTime? PublishedOn { get; set; }
        
        public Publisher Publisher { get; set; }

        public Language Language { get; set; }
        public Format Format { get; set; }

        public string Isbn { get; set; }
        public string DeweyCode { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Cover { get; set; }
        public int? TotalPages { get; set; }
    }
}


