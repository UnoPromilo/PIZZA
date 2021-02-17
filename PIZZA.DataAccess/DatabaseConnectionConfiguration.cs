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
