using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface IBooksRepository
    {
        IEnumerable<Book> List { get; }
        IEnumerable<BookAuthor> BooksAuthors { get; }
        IEnumerable<Language> Languages { get; }
        IEnumerable<Format> Formats { get; }

        void Add(Book book);
        void Delete(Book book);
        void Update(Book book);
    }
}