using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace BookCountry.Models
{
    public class Book
    {
        public Book()
        {
            BooksAuthors = new List<BookAuthor>();
        }
  
        public int Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string Title { get; set; }

        public List<BookAuthor> BooksAuthors { get; set; }

        public string Authors => (from a in BooksAuthors select a.Author.FullName).ToArray().Join(", ");

        public string Edition { get; set; }

        [Required(ErrorMessage = "?")]
        public DateTime? PublishedOn { get; set; }
        
        public Publisher Publisher { get; set; }

        public Language Language { get; set; }
        public Format Format { get; set; }

        [Required(ErrorMessage = "required")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "required")]
        public string DeweyCode { get; set; }

        public decimal? Price { get; set; }

        [Required(ErrorMessage = "required")]
        public int? Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Cover { get; set; }

        [Required(ErrorMessage = "required")]
        public int? TotalPages { get; set; }
    }
}


