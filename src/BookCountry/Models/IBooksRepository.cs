using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetAll();
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
