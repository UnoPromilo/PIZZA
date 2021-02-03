using PIZZA.Models.User;
using System;
using System.Collections.Generic;

namespace PIZZA.Models.Database
{
    public class TaskNote
    {
        public int ID { get; set; }
        public TaskModel Task { get; set; }
        public IEmployee Employee { get; set; }
        public string Note { get; set; }
        public DateTime DateTime { get; set; }
        public TaskNote ResponseTo { get; set; }
        public bool Deleted { get; set; }

        public IEnumerable<TaskNote> Responses { get; set; }
    }
}
