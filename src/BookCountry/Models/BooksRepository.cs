using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BookCountry.Models
{
    public class BooksRepository : IBooksRepository
    {
        private readonly IConfigurationRoot configuration;

        public BooksRepository(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection Connection =>
           new MySqlConnection(configuration["Data:BookCountry:ConnectionString"]);


        // collection of books
        public IEnumerable<Book> List
        {
            get
            {
                using (var connection = Connection)
                {
                    string q = "SELECT b.id, " +
                               "b.title, " +
                               "b.edition, " +
                               "b.publishedOn, " +
                               "b.publisherId, " +
                               "publishers.name as Publisher, " +
                               "languages.name as Language, " +
                               "formats.name as Format, " +
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
                               "INNER JOIN publishers ON b.publisherId = publishers.id " +
                               "INNER JOIN languages ON b.languageId = languages.id " +
                               "INNER JOIN formats ON b.formatId = formats.id";

                    connection.Open();
                    var books = connection.Query<Book>(q).ToList();
                    var authors = BooksAuthors.ToList();
                    foreach (var book in books)
                        book.BooksAuthors = (from a in authors where a.BookId == book.Id select a).ToList();
                    return books;
                }
            }
        }


        // collection of authors and their books (book id)
        public IEnumerable<BookAuthor> BooksAuthors
        {
            get
            {
                using (var connection = Connection)
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



        public void Add(Book book)
        {
            using (var connection = Connection)
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
