using PIZZA.Enums;
using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.Models.Task
{
    public class TaskModelWithActualStateAndCreator : Database.TaskModel
    {
        public TaskState TaskState { get; set; }
        public int TaskCreator { get; set; }
    }
}
