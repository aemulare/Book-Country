using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCountry.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Edition { get; set; }

        [Column("published_on")]
        public DateTime Published_On { get; set; }

        [Column("publisher_id")]
        public int Publisher_Id { get; set; }

        [Column("language_id")]
        public string Language_Id { get; set; }

        [Column("format_id")]
        public int Format_Id { get; set; }

        public string Isbn { get; set; }

        [Column("dewey_code")]
        public string Dewey_Code { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

        [Column("created_at")]
        public DateTime Created_At { get; set; }
    }

}
