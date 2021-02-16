using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.Models.Task
{
    public class NewTaskNoteModel
    {
        public int Task { get; set; }
        public int Employee { get; set; }
        public string Note { get; set; }
        public DateTime DateTime { get; set; }
        public int ResponseTo { get; set; }
    }
}
