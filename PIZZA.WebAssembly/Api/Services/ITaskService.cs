using PIZZA.Models.Database;
using PIZZA.Models.Results;
using PIZZA.Models.Task;
using PIZZA.WebAssembly.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Api.Services
{
    public interface ITaskService
    {
        Task<bool> AddUserToTask(AddUserToTaskModel addUserToTaskModel);
        Task<NewTaskResult> Create(CreateTaskModel createTaskModel);
        Task<bool> CreateTaskNote(NewTaskNoteModel newTaskNoteModel);
        Task<bool> CreateTaskState(NewTaskStateModel newTaskStateModel);
        Task<bool> Delete(int taskID);
        Task<TaskModelWithActualStateAndCreator> Get(int taskID);
        Task<TaskNoteModel> GetLastTaskState(int taskID);
        Task<TaskNoteModel> GetTaskNote(int taskNote);
        Task<IList<TaskNoteModel>> GetTaskNotesForTask(int taskID);
        Task<List<TaskModelWithActualStateAndCreator>> GetTasks(CancellationToken cancellationToken, string query, TaskSearchOptions searchOptions);
        Task<IList<TaskWithTaskRole>> GetTasksForUser(int userID);
        Task<TaskStateModel> GetTaskState(int taskState);
        Task<IList<TaskStateModel>> GetTaskStateHistory(int taskID);
        Task<IList<EmployeeWithTaskRole>> GetUsersInTask(int taskID);
        Task<bool> RemoveTaskNote(int taskNote);
        Task<bool> RemoveTaskState(int taskState);
        Task<bool> RemoveUserFormTask(int taskID, int userID);
        Task<bool> Update(TaskModel taskModel);
        Task<bool> UpdateTaskNote(TaskNoteModel taskNoteModel);
        Task<bool> UpdateTaskState(TaskStateModel taskStateModel);
    }
}
