using PIZZA.Enums;
using System;

namespace PIZZA.Models.Task
{
    public class NewTaskStateModel
    {
        public int Task { get; set; }
        public TaskState NewTaskState { get; set; }
        public string Note { get; set; }
    }
}
