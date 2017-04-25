using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface IBorrowersRepository
    {
        IEnumerable<Borrower> GetAll();

        void Add(Borrower borrower);
        void Delete(Borrower borrower);
        void Update(Borrower borrower);
    }
}