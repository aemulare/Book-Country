using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BookCountry.Models
{
    public class BooksRepository : RepositoryBase, IBooksRepository
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="configuration"></param>
        public BooksRepository(IConfigurationRoot configuration) : base(configuration) { }

        /// <summary>
        /// method GetAll
        /// </summary>
        /// <returns>collection of books</returns>
        public IEnumerable<Book> GetAll()
        {
            using (var connection = GetConnection())
            {
                const string SQL = "SELECT * FROM books as b " +
                           "INNER JOIN formats as f ON b.formatId = f.id " +
                           "INNER JOIN languages as lang ON b.languageId = lang.id " +
                           "INNER JOIN publishers as pub ON b.publisherId = pub.id";

                connection.Open();

                var books = connection.Query<Book,Format,Language,Publisher,Book>(SQL,
                    (book, format, language, publisher) =>
                    {
                        book.Format = format;
                        book.Language = language;
                        book.Publisher = publisher;
                        return book;
                    }).ToList();

                var authors = BooksAuthors.ToList();
                foreach (var book in books)
                {
                    book.BooksAuthors = (from a in authors where a.BookId == book.Id select a).ToList();
                }
                return books;
            }
        }


        /// <summary>
        /// property: collection of authors and their books (book id)
        /// </summary>
        public IEnumerable<BookAuthor> BooksAuthors
        {
            get
            {
                using (var connection = GetConnection())
                {
                    const string SQL = "SELECT * " +
                               "FROM authors as a " +
                               "INNER JOIN books_authors ba ON a.id = ba.authorId ";
                    connection.Open();
                    return connection.Query<Author,BookAuthor,BookAuthor>(SQL,
                        (author, ba) =>
                        {
                            ba.Author = author;
                            return ba;
                        });
                }
            }
        }


        /// <summary>
        /// property: collection of languages
        /// </summary>
        public IEnumerable<Language> Languages
        {
            get
            {
                using (var connection = GetConnection())
                {
                    const string SQL = "SELECT * from languages";
                    connection.Open();
                    return connection.Query<Language>(SQL);
                }
            }
        }


        /// <summary>
        /// property: collection of book formats
        /// </summary>
        public IEnumerable<Format> Formats
        {
            get
            {
                using (var connection = GetConnection())
                {
                    const string SQL = "SELECT * from formats";
                    connection.Open();
                    return connection.Query<Format>(SQL);
                }
            }
        }


        /// <summary>
        /// property: collection of book publishers
        /// </summary>
        public IEnumerable<Publisher> Publishers
        {
            get
            {
                using (var connection = GetConnection())
                {
                    const string SQL = "SELECT * from publishers";
                    connection.Open();
                    return connection.Query<Publisher>(SQL);
                }
            }
        }


        /// <summary>
        /// method Add [new book to the collection]
        /// </summary>
        /// <param name="book"></param>
        public void Add(Book book)
        {
            using (var connection = GetConnection())
            {
                const string SQL =
                    "insert into books" + 
                    "(title, edition, publishedOn, publisherId, languageId, formatId, " +
                    "isbn, deweyCode, price, quantity, createdAt, cover, totalPages) " +
                    "values" +
                    "(@Title, @Edition, @PublishedOn, @PublisherId, @LanguageId, @FormatId, " +
                    "@Isbn, @DeweyCode, @Price, @Quantity, @CreatedAt, @Cover, @TotalPages)";

                connection.Open();
                connection.Execute(SQL, book);
            }
        }


        /// <summary>
        /// methord Delete [book from the collection]
        /// </summary>
        /// <param name="book"></param>
        public void Delete(Book book)
        {
            using (var connection = GetConnection())
            {
                const string SQL = "";

                connection.Open();
                connection.Execute(SQL, book);
            }
        }


        /// <summary>
        /// method Update [book record]
        /// </summary>
        /// <param name="book"></param>
        public void Update(Book book)
        {
            using (var connection = GetConnection())
            {
                const string SQL =
                    "UPDATE books" +
                    "SET title, edition, publishedOn, publisherId, languageId, formatId, " +
                    "isbn, deweyCode, price, quantity, createdAt, cover, totalPages" +
                    "WHERE id =  ";

                connection.Open();
                connection.Execute(SQL, book);
            }
        }
    }
}
