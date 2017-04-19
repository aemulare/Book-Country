using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCountry.Models
{
    public class Book
    {
        [Column("id"), Required]
        public int Id { get; set; }

        [Column("title"), Required]
        public string Title { get; set; }

        [Column("edition")]
        public string Edition { get; set; }

        [Column("publishedOn"), Required]
        public DateTime PublishedOn { get; set; }

        [Column("PublisherId"), Required]
        public int PublisherId { get; set; }

        [ForeignKey("PublisherId"), Required]
        public Publisher Publisher { get; set; }

        [Column("languageId"), Required]
        public string LanguageId { get; set; }

        [Column("formatId")]
        public int FormatId { get; set; }

        [Column("isbn"), Required]
        public string Isbn { get; set; }

        [Column("deweyCode"), Required]
        public string DeweyCode { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("quantity")]
        public int? Quantity { get; set; }

        [Column("createdAt"), Required]
        public DateTime CreatedAt { get; set; }

    }


}


