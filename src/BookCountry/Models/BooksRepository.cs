using System.Collections.Generic;
using System.Data;
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
        /// method FindAuthors
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public List<Author> FindAuthors(string firstName, string lastName)
        {
            using (var connection = GetConnection())
            {
                const string SQL = "SELECT * from authors " +
                    "WHERE firstName = @FirstName and lastName = @LastName";

                connection.Open();
                return connection.Query<Author>(SQL, 
                    new
                    {
                        FirstName = firstName,
                        LastName = lastName
                    }).ToList();
            }
        }



        /// <summary>
        /// method Save [new book to the collection]
        /// </summary>
        /// <param name="book"></param>
        public void Save(Book book)
        {
            using (var connection = GetConnection())
            {
                const string SQL =
                    "insert into books" + 
                    "(title, edition, publishedOn, publisherId, languageId, formatId, " +
                    "isbn, deweyCode, price, quantity, createdAt, cover, totalPages) " +
                    "values" +
                    "(@Title, @Edition, @PublishedOn, @PublisherId, @LanguageId, @FormatId, " +
                    "@Isbn, @DeweyCode, @Price, @Quantity, @CreatedAt, @Cover, @TotalPages); " +
                    "select LAST_INSERT_ID();";

                connection.Open();
                book.Id = connection.Query<int>(SQL,
                    new
                    {
                        Title = book.Title,
                        Edition = book.Edition,
                        PublishedOn = book.PublishedOn,
                        PublisherId = book.Publisher.Id,
                        LanguageId = book.Language.Id,
                        FormatId = book.Format.Id,
                        Isbn = book.Isbn,
                        DeweyCode = book.DeweyCode,
                        Price = book.Price,
                        Quantity = book.Quantity,
                        CreatedAt = book.CreatedAt,
                        Cover = book.Cover,
                        TotalPages = book.TotalPages
                    }).First();

                const string AUTHOR_SQL = "insert into books_authors (bookId, authorId, authorOrdinal) " +
                    "values (@BookId, @AuthorId, @AuthorOrdinal)";
                foreach (var author in book.BooksAuthors)
                {
                    if (author.Author.Id == 0)
                        AddAuthor(author.Author, connection);

                    connection.Execute(AUTHOR_SQL, new
                    {
                        BookId = book.Id,
                        AuthorId = author.Author.Id,
                        AuthorOrdinal = author.AuthorOrdinal
                    });
                }
            }
        }


        /// <summary>
        /// method AddAuthor
        /// </summary>
        /// <param name="author"></param>
        /// <param name="connection"></param>
        private void AddAuthor(Author author, IDbConnection connection)
        {
            const string SQL = "insert into authors(firstName, lastName) " +
                               "values (@FirstName, @LastName); " +
                               "select LAST_INSERT_ID();";
            author.Id = connection.Query<int>(SQL,
                new
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName
                }).First();
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
