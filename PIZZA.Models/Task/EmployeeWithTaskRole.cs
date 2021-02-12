using PIZZA.Enums;
using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.Models.Task
{
    public class EmployeeWithTaskRole
    {
        public EmployeeModel Employee { get; set; }
        public TaskRole Role { get; set; }
    }
}
