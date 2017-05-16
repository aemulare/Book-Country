using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface IBooksRepository
    {
        /// <summary>
        /// Gets all books in the library.
        /// </summary>
        /// <returns>A collection of all books.</returns>
        IEnumerable<Book> GetAll();

        /// <summary>
        /// Gets a book by unique ID.
        /// </summary>
        /// <param name="bookId">Book unique ID.</param>
        /// <returns>Book instance.</returns>
        Book GetById(int bookId);

        IEnumerable<Language> Languages { get; }
        IEnumerable<Format> Formats { get; }
        IEnumerable<Publisher> Publishers { get; }

        List<Author> FindAuthors(string firstName, string lastName);

        void Save(Book book);
        void Update(Book book);

        /// <summary>
        /// Performs books search.
        /// </summary>
        /// <param name="searchTemplate">Search template string.</param>
        /// <returns>A collection of found books.</returns>
        IEnumerable<Book> Search(string searchTemplate);
    }
}
