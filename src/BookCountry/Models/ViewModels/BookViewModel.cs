using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookCountry.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookCountry.ViewModels
{
    public class BookViewModel
    {
        public BookViewModel() : this(new Book())
        {
        }



        public BookViewModel(Book book)
        {
            Book = book;
        }



        public Book Book { get; set; }

        public IEnumerable<SelectListItem> Languages { get; internal set; }
        public IEnumerable<SelectListItem> Formats { get; internal set; }
        public IEnumerable<SelectListItem> Publishers { get; internal set; }


        [Required(ErrorMessage="required")]
        public string Authors { get; set; }

        /// <summary>
        /// The number of book copies available for reservation.
        /// </summary>
        public int AvailableCount { get; set; }

        /// <summary>
        /// Determines whether a book is available for reservation.
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
