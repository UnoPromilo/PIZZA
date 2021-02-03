using PIZZA.Models.User;
using System;
using System.Collections.Generic;

namespace PIZZA.Models.Database
{
    public class TaskState
    {
        public int ID { get; set; }
        public TaskModel Task { get; set; }
        public Enums.TaskState NewTaskState { get; set; }
        public DateTime DateTime { get; set; }
        public IEmployee Editor { get; set; }
        public string TaskNote { get; set; }
    }
}
