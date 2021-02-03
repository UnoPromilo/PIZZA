using PIZZA.Enums;
using System.ComponentModel.DataAnnotations;

namespace PIZZA.Models.Instalation
{
    public class SQLServerConfiguration
    {
        [Required]
        public DatabaseConnectionType DatabaseConnectionType { get; set; }
        public string DatabaseAddress { get; set; } = "localhost";
        public string DatabaseName { get; set; }
        public string DatabaseUsername { get; set; }
        public string DatabasePassword { get; set; }
        public string ConnectionString { get; set; }
    }
}
