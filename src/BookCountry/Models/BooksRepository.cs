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
                               "b.createdAt " +
                               "FROM books b " +
                               "INNER JOIN publishers ON b.publisherId = publishers.id " +
                               "INNER JOIN languages ON b.languageId = languages.id " +
                               "INNER JOIN formats ON b.formatId = formats.id";

                    connection.Open();
                    return connection.Query<Book>(q);
                }
            }
        }

        

        public IDbConnection Connection =>
            new MySqlConnection(configuration["Data:BookCountry:ConnectionString"]);


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
