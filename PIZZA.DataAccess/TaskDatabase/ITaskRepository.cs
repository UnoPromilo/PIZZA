using PIZZA.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.DataAccess.TaskDatabase
{
    public interface ITaskRepository
    {
        Task<int> NewTask(CreateTaskModel createTaskModel, int creatorID);
    }
}
