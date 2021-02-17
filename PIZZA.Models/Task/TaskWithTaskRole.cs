using PIZZA.Enums;
using PIZZA.Models.Database;

namespace PIZZA.Models.Task
{
    public class TaskWithTaskRole
    {
        public TaskModel Task { get; set; }
        public TaskRole Role { get; set; }
    }
}
