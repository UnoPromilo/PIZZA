using PIZZA.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.Models.Task
{
    public class AddUserToTaskModel
    {
        public string UserID { get; set; }
        public string TaskID { get; set; }
        public TaskRole TaskRole { get; set; }
    }
}
