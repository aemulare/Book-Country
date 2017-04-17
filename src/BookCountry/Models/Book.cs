using System;

namespace BookCountry.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int Author_Id { get; set; }
        public string Title { get; set; }
        public DateTime Published_On { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int Binding_Format { get; set; }
        public string Isbn { get; set; }
        public decimal? Price { get; set; }
        public string Dewey_Code { get; set; }
        public DateTime Created_At { get; set; }
    }

}
