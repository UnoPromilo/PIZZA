using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.DataAccess
{
    public partial class DatabaseConnectionConfiguration
    {
        
        public string SqlConnectionString { get; private set;}
  
        public DatabaseConnectionConfiguration(string sqlConnectionString = "")
        {
            SqlConnectionString = sqlConnectionString;
        }   
    }
}
