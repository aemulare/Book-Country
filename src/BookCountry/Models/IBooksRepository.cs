using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface IBooksRepository
    {
        IEnumerable<Book> Books { get; } 
    }
}