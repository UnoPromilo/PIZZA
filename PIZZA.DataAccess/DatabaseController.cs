using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.DataAccess
{
    public class DatabaseController
    {
        protected string sqlConnectionString;

        protected IDbConnection DbConnection
        {
            get
            {
                return new SqlConnection(sqlConnectionString);
            }
        }

        public DatabaseController(DatabaseConnectionConfiguration databaseConnectionConfiguration)
        {
            sqlConnectionString = databaseConnectionConfiguration.SqlConnectionString;

            Helper.OnConnectionStringAcctualization += (sender, connectionString) =>
             {
                 sqlConnectionString = connectionString;
             };
        }  
        

        
    }
}
