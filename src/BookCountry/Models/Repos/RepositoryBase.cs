﻿using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BookCountry.Models
{
    public abstract class RepositoryBase
    {
        // field
        private readonly IConfigurationRoot configuration;

        protected const string IDENTITY_CLAUSE = "select LAST_INSERT_ID();";
        //protected const string IDENTITY_CLAUSE = "select cast(scope_identity() as int);";


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
