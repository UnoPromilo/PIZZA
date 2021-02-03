using Dapper;
using PIZZA.Models.Task;
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

        public async Task<int> NewTask(CreateTaskModel createTaskModel, int creatorID)
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
    }
}
