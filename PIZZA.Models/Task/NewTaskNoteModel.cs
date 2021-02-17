using System;

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
