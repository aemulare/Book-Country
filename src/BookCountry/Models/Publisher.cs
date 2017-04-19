using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCountry.Models
{
    public class Publisher
    {
        [Column("id"), Required]
        public int Id { get; set; }

        [Column("name"), Required]
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
