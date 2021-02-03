using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

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

        public static async Task<bool> CreateDatabaseModelAsync(string connectionString)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "PIZZA.DataAccess.DatabaseModel.sql";

            string sql;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                sql = reader.ReadToEnd();
            }
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                Server server = new Server(new ServerConnection(sqlConnection: connection));
                Task<int> task = new Task<int>(() =>
                {
                    return server.ConnectionContext.ExecuteNonQuery(sql);
                });
                task.Start();
                var rows = await task;
                return rows != 0;
            }

        }

        public static async Task<bool> ClearDatabaseDataAsync(string connectionString)
        {
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                Server server = new Server(new ServerConnection(sqlConnection: connection));
                Task<int> task = new Task<int>(() =>
                {
                    string sql =
                    "EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'\n" +
                    "GO\n" +

                    " EXEC sp_MSForEachTable 'DELETE FROM ?'\n" +
                    " GO\n" +

                    "EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'\n" +
                    "GO\n";
                    return server.ConnectionContext.ExecuteNonQuery(sql);
                });
                task.Start();
                var rows = await task;
                return rows != 0;
            }

        }
    }
}
