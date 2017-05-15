using System.Collections.Generic;

namespace BookCountry.Models.ViewModels
{
    public class BookTilesViewModel
    {
        public BookTilesViewModel()
        {
        }



        public BookTilesViewModel(IEnumerable<Book> books)
        {
            Books = books;
        }



        public string SearchTemplate { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
