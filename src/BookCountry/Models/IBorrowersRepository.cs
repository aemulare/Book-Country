using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface IBorrowersRepository
    {
        IEnumerable<Borrower> GetAll();

        void Create(Borrower borrower);
        void Update(Borrower borrower);
        void Delete(Borrower borrower);
    }
}
