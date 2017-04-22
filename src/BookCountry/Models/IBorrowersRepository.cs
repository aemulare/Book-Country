using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface IBorrowersRepository
    {
        IEnumerable<Borrower> List { get; }
        IEnumerable<Address> Addresses { get; }
    }
}