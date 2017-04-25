using System.Collections.Generic;

namespace BookCountry.Models
{
    public interface ILoansRepository
    {
        IEnumerable<Loan> List { get; }
    }
}
