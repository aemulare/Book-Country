using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BookCountry.Models
{
    public abstract class RepositoryBase
    {
        // field
        private readonly IConfigurationRoot configuration;

        // constructor
        protected RepositoryBase(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        // method
        protected IDbConnection GetConnection() =>
          new MySqlConnection(configuration["Data:BookCountry:ConnectionString"]);
    }
}
