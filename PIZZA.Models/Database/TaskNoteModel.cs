using PIZZA.Models.User;
using System;
using System.Collections.Generic;

namespace PIZZA.Models.Database
{
    public class TaskNoteModel
    {
        public int ID { get; set; }
        public int Task { get; set; }
        public int Employee { get; set; }
        public string Note { get; set; }
        public DateTime DateTime { get; set; }
        public int? ResponseTo { get; set; }
        public bool Deleted { get; set; }

        public IList<TaskNoteModel> Responses { get; set; } = new List<TaskNoteModel>();
    }
}
