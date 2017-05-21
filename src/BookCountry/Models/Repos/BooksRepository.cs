using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BookCountry.Models
{
    public class BooksRepository : RepositoryBase, IBooksRepository
    {
        private const string BOOKS_SQL = "SELECT * FROM books as b " +
            "INNER JOIN formats as f ON b.formatId = f.id " +
            "INNER JOIN languages as lang ON b.languageId = lang.id " +
            "INNER JOIN publishers as pub ON b.publisherId = pub.id " +
            "INNER JOIN books_authors as ba ON ba.bookId = b.id " +
            "INNER JOIN authors as aut ON aut.id = ba.authorId";

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="configuration"></param>
        public BooksRepository(IConfigurationRoot configuration) : base(configuration) { }

        /// <summary>
        /// method GetAll
        /// </summary>
        /// <returns>collection of books</returns>
        public IEnumerable<Book> GetAll() => QueryBooks(BOOKS_SQL);

        /// <summary>
        /// Gets a book by unique ID.
        /// </summary>
        /// <param name="bookId">Book unique ID.</param>
        /// <returns>Book instance.</returns>
        public Book GetById(int bookId) => QueryBooks(BOOKS_SQL + " WHERE b.id = @BookId;",
            new { BookId = bookId }).FirstOrDefault();



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
                        book.Title,
                        book.Edition,
                        book.PublishedOn,
                        PublisherId = book.Publisher.Id,
                        LanguageId = book.Language.Id,
                        FormatId = book.Format.Id,
                        book.Isbn,
                        book.DeweyCode,
                        book.Price,
                        book.Quantity,
                        book.CreatedAt,
                        book.Cover,
                        book.TotalPages
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
                        author.AuthorOrdinal
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
            const string SQL = "INSERT into authors(firstName, lastName) " +
                               "values (@FirstName, @LastName); " +
                               "select LAST_INSERT_ID();";
            author.Id = connection.Query<int>(SQL,
                new
                {
                    author.FirstName,
                    author.LastName
                }).First();
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



        /// <summary>
        /// Performs books search.
        /// </summary>
        /// <param name="searchTemplate">Search template string.</param>
        /// <returns>A collection of found books.</returns>
        public IEnumerable<Book> Search(string searchTemplate)
        {
            const string BOOKS_SEARCH_SQL = BOOKS_SQL + " WHERE " +
                "b.title like @SearchTemplate OR aut.lastName = @Author OR " +
                "b.isbn = @Isbn OR b.deweyCode like @DeweyCode OR " +
                "lang.name = @Language";

            var text = searchTemplate.Trim();
            return QueryBooks(BOOKS_SEARCH_SQL,
                new
                {
                    SearchTemplate = $"%{text}%",
                    Author = text,
                    Isbn = text,
                    DeweyCode = $"{text}%",
                    Language = text
                });
        }



        private IEnumerable<Book> QueryBooks(string sql, object param=null)
        {
            using(var connection = GetConnection())
            {
                connection.Open();
                var books = connection.Query<Book,Format,Language,Publisher,BookAuthor,Author,Book>(sql,
                    (book, format, language, publisher, bookAuthor, author) =>
                    {
                        book.Format = format;
                        book.Language = language;
                        book.Publisher = publisher;
                        bookAuthor.Author = author;
                        book.BooksAuthors.Add(bookAuthor);
                        return book;
                    }, param)
                    .GroupBy(book => book.Id)
                    .Select(group =>
                    {
                        var combinedBook = group.First();
                        combinedBook.BooksAuthors = group.Select(book => book.BooksAuthors.Single()).ToList();
                        return combinedBook;
                    })
                    .ToList();

                return books;
            }
        }
    }
}
