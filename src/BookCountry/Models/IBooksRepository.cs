using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetAll();
        IEnumerable<BookAuthor> BooksAuthors { get; }
        IEnumerable<Language> Languages { get; }
        IEnumerable<Format> Formats { get; }
        IEnumerable<Publisher> Publishers { get; }

        List<Author> FindAuthors(string firstName, string lastName);
             
        void Save(Book book);
        void Delete(Book book);
        void Update(Book book);
        
    }
}