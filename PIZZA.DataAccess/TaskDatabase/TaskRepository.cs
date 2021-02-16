using Dapper;
using PIZZA.Enums;
using PIZZA.Models.Database;
using PIZZA.Models.Task;
using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.DataAccess.TaskDatabase
{
    public class TaskRepository : DatabaseController, ITaskRepository
    {
        public TaskRepository(DatabaseConnectionConfiguration databaseConnectionConfiguration) : base(databaseConnectionConfiguration) { }

        public async Task<int> NewTask(CreateTaskModel createTaskModel, string creatorID)
        {
            int output;
            using(var cnn = DbConnection)
            {
                var procedure = "[CreateTask]";
                var values = new
                {
                    Creator = creatorID,
                    createTaskModel.Deadline,
                    createTaskModel.Priority,
                    createTaskModel.Name,
                    createTaskModel.Description,
                    createTaskModel.Note
                };
                output = await cnn.ExecuteAsync(procedure, values, commandType: CommandType.StoredProcedure);
            }
            return output;
        }
   
        public async Task<int> UpdateTask(TaskModel taskModel)
        {
            int rows;
            using (var cnn = DbConnection)
            {
                var procedure = "[UpdateTask]";
                rows = await cnn.ExecuteAsync(procedure, taskModel, commandType: CommandType.StoredProcedure);
            }
            return rows;
        }

        public async Task<int> AddUserToTask(string taskID, string userID, TaskRole role)
        {
            int rows;
            using (var cnn = DbConnection)
            {
                var procedure = "[AddUserToTask]";
                var values = new
                {
                    Task = taskID,
                    Employee = userID,
                    Role = (int)role
                };
                rows = await cnn.ExecuteAsync(procedure, values, commandType: CommandType.StoredProcedure);
            }
            return rows;
        }

        public async Task<TaskModelWithActualStateAndCreator> FindTaskByID(string taskID)
        {
            TaskModelWithActualStateAndCreator output;
            using (var cnn = DbConnection)
            {
                var procedure = "[FindTaskByID]";
                var values = new
                {
                    ID = taskID
                };
                output = await cnn.QuerySingleOrDefaultAsync<TaskModelWithActualStateAndCreator>(procedure, values, commandType: CommandType.StoredProcedure);
            }
            return output;
        }

        public async Task<IList<TaskWithTaskRole>> GetTasksForUser(string userID)
        {
            IList<TaskWithTaskRole> output = new List<TaskWithTaskRole>();

            using (var cnn = DbConnection)
            {
                var procedure = "[GetTasksForUser]";
                var values = new
                {
                    UserID = userID
                };
                var enumerable = await cnn.QueryAsync<TaskRole,
                                                    TaskModel,
                                                    KeyValuePair<TaskModel, TaskRole>>
                    (procedure,
                    (role, task) => new KeyValuePair<TaskModel, TaskRole>(task, role),
                    values,
                    commandType: CommandType.StoredProcedure);
                foreach (var item in enumerable)
                    output.Add(new TaskWithTaskRole
                    {
                        Task = item.Key,
                        Role = item.Value
                    });
            }

            return output;
        }

        public async Task<IList<EmployeeWithTaskRole>> GetUsersInTask(string taskID)
        {
            IList<EmployeeWithTaskRole> output = new List<EmployeeWithTaskRole>();

            using (var cnn = DbConnection)
            {
                var procedure = "[GetUsersInTask]";
                var values = new
                {
                    TaskID = taskID
                };
                var enumerable =await cnn.QueryAsync<TaskRole,
                                                    EmployeeModel,
                                                    KeyValuePair<EmployeeModel, TaskRole>>
                    (procedure,
                    (role, employee) => new KeyValuePair<EmployeeModel, TaskRole>(employee, role),
                    values,
                    commandType: CommandType.StoredProcedure);

                foreach (var item in enumerable)
                    output.Add(new EmployeeWithTaskRole
                    {
                        Employee = item.Key,
                        Role = item.Value
                    });
            }

            return output;
        }

        public async Task<int> RemoveTask(string taskID)
        {
            int rows;
            using (var cnn = DbConnection)
            {
                var procedure = "[RemoveTask]";
                var values = new
                {
                    ID = taskID
                };
                rows = await cnn.ExecuteAsync(procedure, values, commandType: CommandType.StoredProcedure);
            }
            return rows;
        }

        public async Task<int> RemoveUserFromTask(string taskID, string userID)
        {
            int rows;
            using (var cnn = DbConnection)
            {
                var procedure = "[RemoveUserFromTask]";
                var values = new
                {
                    UserID = userID,
                    TaskID = taskID
                };
                rows = await cnn.ExecuteAsync(procedure, values, commandType: CommandType.StoredProcedure);
            }
            return rows;
        }

        public async Task<int> AddTaskState(NewTaskStateModel model)
        {
            int rows;
            using(var cnn = DbConnection)
            {
                var procedure = "[AddTaskState]";
                rows = await cnn.ExecuteAsync(procedure, model, commandType: CommandType.StoredProcedure);
            }
            return rows;
        }
        public async Task<TaskStateModel> FindTaskStateByID(string ID)
        {
            TaskStateModel output;
            using (var cnn = DbConnection)
            {
                var procedure = "[FindTaskStateByID]";
                var values = new
                {
                    ID
                };
                output = await cnn.QueryFirstOrDefaultAsync<TaskStateModel>(procedure, values, commandType: CommandType.StoredProcedure);
            }
            return output;
        }

        public async Task<TaskStateModel> GetTaskState(string ID)
        {
            TaskStateModel output;
            using (var cnn = DbConnection)
            {
                var procedure = "[FindTaskStateById]";
                var values = new
                {
                    ID
                };
                output = await cnn.QueryFirstOrDefaultAsync<TaskStateModel>(procedure, values, commandType: CommandType.StoredProcedure);
            }
            return output;
        }

        public async Task<TaskStateModel> GetLastTaskState(string taskID)
        {
            TaskStateModel output;
            using (var cnn = DbConnection)
            {
                var procedure = "[GetLastTaskState]";
                var values = new
                {
                    TaskID = taskID
                };
                output = await cnn.QueryFirstOrDefaultAsync<TaskStateModel>(procedure, values, commandType: CommandType.StoredProcedure);
            }
            return output;
        }
        public async Task<IList<TaskStateModel>> GetTaskStateHistory(string taskID)
        {
            IEnumerable<TaskStateModel> output;
            using (var cnn = DbConnection)
            {
                var procedure = "[GetTaskStateHistory]";
                var values = new
                {
                    TaskID = taskID
                };
                output = await cnn.QueryAsync<TaskStateModel>(procedure, values, commandType: CommandType.StoredProcedure);
            }
            return output.ToList();
        }
        public async Task<int> RemoveTaskState(string ID)
        {
            int rows;
            using (var cnn = DbConnection)
            {
                var procedure = "[RemoveTaskState]";
                var values = new
                {
                    ID
                };
                rows = await cnn.ExecuteAsync(procedure, values, commandType: CommandType.StoredProcedure);
            }
            return rows;
        }

        public async Task<int> UpdateTaskState(TaskStateModel model)
        {
            int rows;
            using (var cnn = DbConnection)
            {
                var procedure = "[RemoveTaskState]";
                var values = new
                {
                    model.ID,
                    model.NewTaskState,
                    model.DateTime,
                    model.Editor
                };
                rows = await cnn.ExecuteAsync(procedure, values, commandType: CommandType.StoredProcedure);
            }
            return rows;
        }

        public async Task<int> AddTaskNote(NewTaskNoteModel model)
        {
            int rows;
            using (var cnn = DbConnection)
            {
                var procedure = "[AddTaskNote]";
                rows = await cnn.ExecuteAsync(procedure, model, commandType: CommandType.StoredProcedure);
            }
            return rows;
        }
        public async Task<IList<TaskNoteModel>> GetTaskNotesForTask(string taskID)
        {
            IEnumerable<TaskNoteModel> output;
            using (var cnn = DbConnection)
            {
                var procedure = "[GetNotesForTask]";
                var values = new
                {
                    TaskID = taskID
                };
                output = await cnn.QueryAsync<TaskNoteModel>(procedure, values, commandType: CommandType.StoredProcedure);
            }

            Dictionary<int, TaskNoteModel> dict = new Dictionary<int, TaskNoteModel>();
            IList<TaskNoteModel> response = new List<TaskNoteModel>(); ;
            foreach (var item in output)
                dict.Add(item.ID, item);
            foreach(var item in output)
            {
                if (dict.ContainsKey(item.ResponseTo ?? 0))
                    dict[item.ResponseTo ?? 0].Responses.Add(item);
                else
                    response.Add(item);
            }
            return response;
        }

        public async Task<TaskNoteModel> GetTaskNote(string taskNoteID)
        {
            TaskNoteModel mainTask;
            IList<TaskNoteModel> allTasks;
            using (var cnn = DbConnection)
            {
                var procedure = "[GetTaskNote]";
                var values = new
                {
                    ID = taskNoteID
                };
                mainTask = await cnn.QueryFirstOrDefaultAsync<TaskNoteModel>(procedure, values, commandType: CommandType.StoredProcedure);
            }
            if (mainTask == default)
                return default;

            allTasks = await GetTaskNotesForTask(mainTask.ID.ToString());


            Dictionary<int, TaskNoteModel> dict = new Dictionary<int, TaskNoteModel>();
            IList<TaskNoteModel> response = new List<TaskNoteModel>(); ;
            foreach (var item in allTasks)
                dict.Add(item.ID, item);
            foreach (var item in allTasks)
            {
                if (dict.ContainsKey(item.ResponseTo ?? 0))
                    dict[item.ResponseTo ?? 0].Responses.Add(item);
                else
                    response.Add(item);
            }
            return dict[mainTask.ID];
        }

        public async Task<int> RemoveTaskNote(string ID)
        {
            int rows;
            using (var cnn = DbConnection)
            {
                var procedure = "[RemoveTaskNote]";
                var values = new
                {
                    ID
                };
                rows = await cnn.ExecuteAsync(procedure, values, commandType: CommandType.StoredProcedure);
            }
            return rows;
        }

        public async Task<int> UpdateTaskNote(TaskNoteModel model)
        {
            int rows;
            using (var cnn = DbConnection)
            {
                var procedure = "[RemoveTaskState]";
                var values = new
                {
                    model.ID,
                    model.Note,
                    model.DateTime,
                    model.ResponseTo
                };
                rows = await cnn.ExecuteAsync(procedure, values, commandType: CommandType.StoredProcedure);
            }
            return rows;
        }


        public async Task<IList<TaskModelWithActualStateAndCreator>> GetTasks(string query, bool showFinised, int employee = 0)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                query = "";
            }
            IEnumerable<TaskModelWithActualStateAndCreator> output;
            using (var cnn = DbConnection)
            {
                var procedure = "[FindTaskByQuery]";
                DataTable keywords = new DataTable();
                keywords.Columns.Add("keyword", typeof(string));
                foreach (var key in query.Split(null))
                    keywords.Rows.Add(key);

                var parameter = new
                {
                    Keywords = keywords,
                    ShowFinished = showFinised,
                    UserID = employee
                };
                output = await cnn.QueryAsync<TaskModelWithActualStateAndCreator>(
                    procedure,
                    parameter,
                    commandType: CommandType.StoredProcedure);
                    
            }
            return output.AsList();
        }
    }
}
