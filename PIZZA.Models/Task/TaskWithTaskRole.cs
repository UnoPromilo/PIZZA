using PIZZA.Enums;
using PIZZA.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.Models.Task
{
    public class TaskWithTaskRole
    {
        public TaskModel Task { get; set; }
        public TaskRole Role { get; set; }
    }
}
