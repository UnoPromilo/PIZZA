using PIZZA.Enums;

namespace PIZZA.Models.Task
{
    public class AddUserToTaskModel
    {
        public string UserID { get; set; }
        public string TaskID { get; set; }
        public TaskRole TaskRole { get; set; }
    }
}
