using PIZZA.Enums;
using PIZZA.Models.User;

namespace PIZZA.Models.Database
{
    public class EmployeeTask
    {
        public EmployeeModel Employee { get; set; }
        public TaskModel Task { get; set; }
        public TaskRole TaskRole { get; set; }
    }
}
