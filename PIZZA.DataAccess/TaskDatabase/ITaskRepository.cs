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
        Task<int> AddTaskNote(NewTaskNoteModel model);
        Task<int> AddTaskState(NewTaskStateModel model);
        Task<int> AddUserToTask(string taskID, string userID, TaskRole role);
        Task<TaskModelWithActualStateAndCreator> FindTaskByID(string taskID);
        Task<TaskStateModel> GetLastTaskState(string taskID);
        Task<TaskNoteModel> GetTaskNote(string taskNoteID);
        Task<IList<TaskNoteModel>> GetTaskNotesForTask(string taskID);
        Task<IList<TaskModelWithActualStateAndCreator>> GetTasks(string query, bool showFinised, int employee = 0);
        Task<IList<TaskWithTaskRole>> GetTasksForUser(string userID);
        Task<TaskStateModel> GetTaskState(string ID);
        Task<IList<TaskStateModel>> GetTaskStateHistory(string taskID);
        Task<IList<EmployeeWithTaskRole>> GetUsersInTask(string taskID);
        Task<int> NewTask(CreateTaskModel createTaskModel, string creatorID);
        Task<int> RemoveTask(string taskID);
        Task<int> RemoveTaskNote(string ID);
        Task<int> RemoveTaskState(string ID);
        Task<int> RemoveUserFromTask(string taskID, string userID);
        Task<int> UpdateTask(TaskModel taskModel);
        Task<int> UpdateTaskNote(TaskNoteModel model);
        Task<int> UpdateTaskState(TaskStateModel model);
    }
}
