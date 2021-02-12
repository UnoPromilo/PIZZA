using PIZZA.Enums;
using PIZZA.Models.Database;
using PIZZA.Models.Task;
using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.DataAccess.TaskDatabase
{
    public interface ITaskRepository
    {
        Task<int> AddUserToTask(string taskID, string userID, TaskRole role);
        Task<TaskModelWithActualStateAndCreator> FindTaskByID(string taskID);
        Task<IList<TaskWithTaskRole>> GetTasksForUser(string userID);
        Task<IList<EmployeeWithTaskRole>> GetUsersInTask(string taskID);
        Task<int> NewTask(CreateTaskModel createTaskModel, string creatorID);
        Task<int> RemoveTask(string taskID);
        Task<int> RemoveUserFromTask(string taskID, string userID);
        Task<int> UpdateTask(TaskModel taskModel);
    }
}
