using System;
using System.Collections.Generic;

namespace BookCountry.Models
{
    public class Book
    { 
        public int Id { get; set; }
        public string Title { get; set; }

        public List<BookAuthor> BooksAuthors { get; set; }

        public string Edition { get; set; }
        public DateTime PublishedOn { get; set; }
        public int PublisherId { get; set; }
        public string Publisher { get; set; }

        public int LanguageId { get; set; }
        public string Language { get; set; }

        public int FormatId { get; set; }
        public string Format { get; set; }

        public string Isbn { get; set; }
        public string DeweyCode { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}


