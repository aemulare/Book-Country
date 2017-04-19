using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface IBooksRepository
    {
        IEnumerable<Book> List { get; }
        void Add(Book book);
        void Delete(Book book);
        void Update(Book book);
    }
}