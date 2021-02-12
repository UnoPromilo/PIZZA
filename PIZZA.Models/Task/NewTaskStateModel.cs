using PIZZA.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.Models.Task
{
    public class NewTaskStateModel
    {
        public int Task { get; set; }
        public TaskState NewTaskState { get; set; }
        public DateTime DateTime { get; set; }
        public int Editor { get; set; }
        public string Note { get; set; }
    }
}
