﻿using PIZZA.Enums;
using PIZZA.Models.User;
using System;

namespace PIZZA.Models.Database
{
    public class TaskStateModel
    {
        public int ID { get; set; }
        public int Task { get; set; }
        public TaskState NewTaskState { get; set; }
        public DateTime DateTime { get; set; }
        public int Editor { get; set; }
        public EmployeeModel EditorModel { get; set; }
        public int TaskNote { get; set; }
    }
}
