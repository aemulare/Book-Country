using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface ILoansRepository
    {
        IEnumerable<Loan> GetAll();

        void Add(Loan loan);
        void Delete(Loan loan);
        void Update(Loan loan);
    }
}
