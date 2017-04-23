using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface IBorrowersRepository
    {
        IEnumerable<Borrower> List { get; }
        IEnumerable<Address> Addresses { get; }

        //void Add(Borrower borrower);
        //void Delete(Borrower borrower);
        //void Update(Borrower borrower);
    }
}