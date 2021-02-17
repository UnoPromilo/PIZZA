using System.Data;
using System.Data.SqlClient;

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
