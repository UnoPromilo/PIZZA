using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace PIZZA.DataAccess
{
    public static class Helper
    {
        public static bool TestConnection(string connectionString)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                if (db.State == ConnectionState.Open) return true;
                else return false;
            }
        }
    }
}
