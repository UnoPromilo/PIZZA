using PIZZA.Enums;
using PIZZA.Models.User;

namespace PIZZA.Models.Task
{
    public class EmployeeWithTaskRole
    {
        public EmployeeModel Employee { get; set; }
        public TaskRole Role { get; set; }

        public int Task { get; set; }
    }
}
