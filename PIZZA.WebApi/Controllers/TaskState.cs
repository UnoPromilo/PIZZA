using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PIZZA.DataAccess.TaskDatabase;
using PIZZA.Models.Database;
using PIZZA.Models.Task;
using System.Threading.Tasks;

namespace PIZZA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TaskState : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskState(ITaskRepository taskRepository, UserManager<ApplicationUser> userManager)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewTaskStateModel taskStateModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var rowsAffected = await _taskRepository.AddTaskState(taskStateModel, user.ID);
            if (rowsAffected == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Update([FromBody] TaskStateModel taskStateModel)
        {
            var rowsAffected = await _taskRepository.UpdateTaskState(taskStateModel);
            if (rowsAffected == 0)
                return BadRequest();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskState([FromQuery] int taskState)
        {
            var taskStateModel = await _taskRepository.GetTaskState(taskState.ToString());
            if (taskStateModel == default)
                return NotFound();
            return Ok(taskStateModel);
        }

        [HttpDelete]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Remove([FromQuery] int taskState)
        {
            var rows = await _taskRepository.RemoveTaskState(taskState.ToString());
            if (rows == 0)
                return NotFound();
            return Ok();
        }

        [Route("GetLastTaskState")]
        [HttpGet]
        public async Task<IActionResult> GetLastTaskState([FromQuery] int taskID)
        {
            var taskStateModel = await _taskRepository.GetLastTaskState(taskID.ToString());
            if (taskStateModel == default)
                return NotFound();
            return Ok(taskStateModel);
        }

        [Route("GetTaskStateHistory")]
        [HttpGet]
        public async Task<IActionResult> GetTaskStateHistory([FromQuery] int taskID)
        {
            var taskStates = await _taskRepository.GetTaskStateHistory(taskID.ToString());
            if (taskStates == default)
                return NotFound();
            return Ok(taskStates);
        }


    }
}
