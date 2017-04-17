using System.Collections.Generic;

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
