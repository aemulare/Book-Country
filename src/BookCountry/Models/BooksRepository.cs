using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BookCountry.Models
{
    public class BooksRepository : RepositoryBase, IBooksRepository
    {
        // constructor
        public BooksRepository(IConfigurationRoot configuration) : base(configuration) { }
  
        // method - collection of books
        public IEnumerable<Book> GetAll()
        {
            using (var connection = GetConnection())
            {
                string q = "SELECT b.id, " +
                           "b.title, " +
                           "b.edition, " +
                           "b.publishedOn, " +
                           "b.publisherId, " +
                           "publishers.name as Publisher, " +
                           "b.languageId, " +
                           "b.formatId, " +
                           "b.isbn, " +
                           "b.deweyCode, " +
                           "b.price, " +
                           "b.quantity, " +
                           "b.createdAt, " +
                           "b.cover, " +
                           "b.totalPages " +
                           "FROM books b " +
                           "INNER JOIN publishers ON b.publisherId = publishers.id";

                connection.Open();
                var books = connection.Query<Book>(q).ToList();
                var authors = BooksAuthors.ToList();
                var languages = Languages.ToList();
                var formats = Formats.ToList();
                foreach (var book in books)
                {
                    book.BooksAuthors = (from a in authors where a.BookId == book.Id select a).ToList();
                    book.Language = languages.FirstOrDefault(l => l.Id == book.LanguageId);
                    book.Format = formats.FirstOrDefault(f => f.Id == book.FormatId);
                }
                return books;
            }
        }


        // collection of authors and their books (book id)
        public IEnumerable<BookAuthor> BooksAuthors
        {
            get
            {
                using (var connection = GetConnection())
                {
                    string q = "SELECT a.id, " +
                               "a.firstName, " +
                               "a.middleName, " +
                               "a.lastName, " +
                               "ba.authorOrdinal, " +
                               "ba.authorId, " +
                               "ba.bookId, " +
                               "ba.role " +
                               "FROM authors a " +
                               "INNER JOIN books_authors ba ON a.id = ba.authorId " +
                               "INNER JOIN books b ON b.id = ba.bookId";
                    connection.Open();
                    return connection.Query<BookAuthor>(q);
                }
            }
        }

        // collection of languages
        public IEnumerable<Language> Languages
        {
            get
            {
                using (var connection = GetConnection())
                {
                    string q = "SELECT * from languages";
                    connection.Open();
                    return connection.Query<Language>(q);
                }
            }
        }


        // collection of book binding formats
        public IEnumerable<Format> Formats
        {
            get
            {
                using (var connection = GetConnection())
                {
                    string q = "SELECT * from formats";
                    connection.Open();
                    return connection.Query<Format>(q);
                }
            }
        }

        public void Add(Book book)
        {
            using (var connection = GetConnection())
            {
                string q =
                    "insert into books(title, edition, publishedOn, publisherId, languageId, formatId, isbn, deweyCode, price, quantity, createdAt) " +
                    "values(@Title, @Edition, @PublishedOn, @PublisherId, @LanguageId, @FormatId, @Isbn, @DeweyCode, @Price, @Quantity, @CreatedAt)";

                connection.Open();
                connection.Execute(q, book);
            }
        }


        public void Delete(Book book)
        {
            
        }

        public void Update(Book book)
        {
            
        }
    }
}
