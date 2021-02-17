using PIZZA.Enums;

namespace PIZZA.Models.Task
{
    public class TaskModelWithActualStateAndCreator : Database.TaskModel
    {
        public TaskState TaskState { get; set; }
        public int TaskCreator { get; set; }
    }
}
