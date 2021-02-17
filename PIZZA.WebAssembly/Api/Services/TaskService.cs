using Blazored.LocalStorage;
using PIZZA.Models.Database;
using PIZZA.Models.Results;
using PIZZA.Models.Task;
using PIZZA.WebAssembly.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Api.Services
{
    public class TaskService : AuthorizedService, ITaskService
    {
        public TaskService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage) : base(httpClientFactory, localStorage) { }


        public async Task<List<TaskModelWithActualStateAndCreator>> GetTasks(CancellationToken cancellationToken, string query, TaskSearchOptions searchOptions)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await loadAuthTask;
            var httpQuery = $"api/TaskModel/Search?query={query}&showFinished={!searchOptions.ShowOnlyNotFinished}&showUnassigned={searchOptions.ShowUnassignedToMe}";
            var response = await _httpClient.GetAsync(httpQuery, cancellationToken);
            var text = await response.Content.ReadAsStringAsync(cancellationToken);
            var output = JsonSerializer.Deserialize<List<TaskModelWithActualStateAndCreator>>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }

        public async Task<NewTaskResult> Create(CreateTaskModel createTaskModel)
        {
            await loadAuthTask;
            var content = JsonSerializer.Serialize(createTaskModel);
            var response = await _httpClient.PostAsync($"api/TaskModel", new StringContent(content, Encoding.UTF8, "application/json"));
            var text = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<NewTaskResult>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }

        public async Task<bool> Update(TaskModel taskModel)
        {
            await loadAuthTask;
            var content = JsonSerializer.Serialize(taskModel);
            var response = await _httpClient.PutAsync($"api/TaskModel", new StringContent(content, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int taskID)
        {
            await loadAuthTask;
            var response = await _httpClient.DeleteAsync($"api/TaskModel?taskID={taskID}");
            return response.IsSuccessStatusCode;
        }

        public async Task<TaskModelWithActualStateAndCreator> Get(int taskID)
        {
            await loadAuthTask;
            var httpQuery = $"api/TaskModel?taskID={taskID}";
            var response = await _httpClient.GetAsync(httpQuery);
            var text = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<TaskModelWithActualStateAndCreator>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }

        public async Task<bool> AddUserToTask(AddUserToTaskModel addUserToTaskModel)
        {
            await loadAuthTask;
            var content = JsonSerializer.Serialize(addUserToTaskModel);
            var response = await _httpClient.PutAsync($"api/TaskModel/AddUserToTask", new StringContent(content, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<IList<EmployeeWithTaskRole>> GetUsersInTask(int taskID)
        {
            await loadAuthTask;
            var httpQuery = $"api/TaskModel/UsersInTask?taskID={taskID}";
            var response = await _httpClient.GetAsync(httpQuery);
            var text = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<List<EmployeeWithTaskRole>>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }

        public async Task<IList<TaskWithTaskRole>> GetTasksForUser(int userID)
        {
            await loadAuthTask;
            var httpQuery = $"api/TaskModel/TasksForUsers?userID={userID}";
            var response = await _httpClient.GetAsync(httpQuery);
            var text = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<List<TaskWithTaskRole>>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }

        public async Task<bool> RemoveUserFormTask(int taskID, int userID)
        {
            await loadAuthTask;
            var response = await _httpClient.DeleteAsync($"api/TaskModel/RemoveUserFromTask?taskID={taskID}&userID={userID}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateTaskNote(NewTaskNoteModel newTaskNoteModel)
        {
            await loadAuthTask;
            var content = JsonSerializer.Serialize(newTaskNoteModel);
            var response = await _httpClient.PostAsync($"api/TaskNote", new StringContent(content, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTaskNote(TaskNoteModel taskNoteModel)
        {
            await loadAuthTask;
            var content = JsonSerializer.Serialize(taskNoteModel);
            var response = await _httpClient.PutAsync($"api/TaskNote", new StringContent(content, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<TaskNoteModel> GetTaskNote(int taskNote)
        {
            await loadAuthTask;
            var httpQuery = $"api/TaskNote?taskNote={taskNote}";
            var response = await _httpClient.GetAsync(httpQuery);
            var text = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<TaskNoteModel>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }

        public async Task<bool> RemoveTaskNote(int taskNote)
        {
            await loadAuthTask;
            var response = await _httpClient.DeleteAsync($"api/TaskNote?taskNote={taskNote}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IList<TaskNoteModel>> GetTaskNotesForTask(int taskID)
        {
            await loadAuthTask;
            var httpQuery = $"api/TaskNote/GetNotesForTask?taskID={taskID}";
            var response = await _httpClient.GetAsync(httpQuery);
            var text = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<IList<TaskNoteModel>>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }

        public async Task<bool> CreateTaskState(NewTaskStateModel newTaskStateModel)
        {
            await loadAuthTask;
            var content = JsonSerializer.Serialize(newTaskStateModel);
            var response = await _httpClient.PostAsync($"api/TaskState", new StringContent(content, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTaskState(TaskStateModel taskStateModel)
        {
            await loadAuthTask;
            var content = JsonSerializer.Serialize(taskStateModel);
            var response = await _httpClient.PutAsync($"api/TaskState", new StringContent(content, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<TaskStateModel> GetTaskState(int taskState)
        {
            await loadAuthTask;
            var httpQuery = $"api/TaskState?taskState={taskState}";
            var response = await _httpClient.GetAsync(httpQuery);
            var text = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<TaskStateModel>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }

        public async Task<bool> RemoveTaskState(int taskState)
        {
            await loadAuthTask;
            var response = await _httpClient.DeleteAsync($"api/TaskState?taskState={taskState}");
            return response.IsSuccessStatusCode;
        }

        public async Task<TaskNoteModel> GetLastTaskState(int taskID)
        {
            await loadAuthTask;
            var httpQuery = $"api/TaskState/GetLastTaskState?taskID={taskID}";
            var response = await _httpClient.GetAsync(httpQuery);
            var text = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<TaskNoteModel>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }

        public async Task<IList<TaskStateModel>> GetTaskStateHistory(int taskID)
        {
            await loadAuthTask;
            var httpQuery = $"api/TaskState/GetTaskStateHistory?taskID={taskID}";
            var response = await _httpClient.GetAsync(httpQuery);
            var text = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<IList<TaskStateModel>>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }
    }
}
