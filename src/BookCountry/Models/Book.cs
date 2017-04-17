using System;

namespace BookCountry.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int BindingFormat { get; set; }
        public string Isbn { get; set; }
        public decimal Price { get; set; }
        public string DeweyCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
