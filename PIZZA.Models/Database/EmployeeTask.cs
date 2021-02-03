using PIZZA.Enums;
using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.Models.Database
{
    public class EmployeeTask
    {
        public EmployeeModel Employee { get; set; }
        public TaskModel Task { get; set; }
        public TaskRole TaskRole { get; set; }
    }
}
