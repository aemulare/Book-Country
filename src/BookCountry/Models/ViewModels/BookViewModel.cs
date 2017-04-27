using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookCountry.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookCountry.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> Languages { get; internal set; }
        public IEnumerable<SelectListItem> Formats { get; internal set; }
        public IEnumerable<SelectListItem> Publishers { get; internal set; }


        [Required(ErrorMessage = "required")]
        public string Authors { get; set; }

        public BookViewModel()
        {
            Book = new Book();
        }
    }
}
