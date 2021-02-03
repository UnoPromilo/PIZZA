using PIZZA.Enums;
using System;

namespace PIZZA.Models.Database
{
    public class TaskModel
    {
        public int ID { get; set; }
        public DateTime Deadline { get; set; }
        public TaskPriority Priority { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //public IEnumerable<TaskNote>      TaskNotes   { get; set; }
        //public IEnumerable<TaskState>     TaskStates  { get; set; }
        //public IEnumerable<EmployeeTask>  Employees   { get; set; }
    }
}
