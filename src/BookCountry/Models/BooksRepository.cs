using System.Collections.Generic;
using System.Linq;

namespace BookCountry.Models
{
    public class BooksRepository : IBooksRepository
    {
        private readonly AppDbContext dbContext;

        public BooksRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IEnumerable<Book> Books
        {
            get { return dbContext.Books; }
        }
    }
}
