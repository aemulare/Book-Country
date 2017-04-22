using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface IBorrowersRepository
    {
        IEnumerable<Borrower> List { get; }
        IEnumerable<Address> Addresses { get; }

        //void Add(Book book);
        //void Delete(Book book);
        //void Update(Book book);
    }
}