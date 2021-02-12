using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PIZZA.DataAccess.TaskDatabase;
using PIZZA.Models.Database;
using PIZZA.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager, Admin")]
    public class TaskModel : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskModel(UserManager<ApplicationUser> userManager, ITaskRepository taskRepository)
        {
            _userManager = userManager;
            _taskRepository = taskRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskModel createTaskModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var rowsAffected = await _taskRepository.NewTask(createTaskModel, user.ID.ToString());
            if (rowsAffected == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Models.Database.TaskModel taskModel)
        {
            var rows = await _taskRepository.UpdateTask(taskModel);
            if (rows > 0)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int taskID)
        {
            var rows = await _taskRepository.RemoveTask(taskID.ToString());
            if (rows > 0)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int taskID)
        {
            var task = await _taskRepository.FindTaskByID(taskID.ToString());
            if (task == default || task.ID != taskID)
                return NotFound();
            else
                return Ok(task);
        }

        [Route("addUserToTask")]
        [HttpPut]
        public async Task<IActionResult> AddUserToTask([FromBody] AddUserToTaskModel model)
        {
            var rows = await _taskRepository.AddUserToTask(model.TaskID, model.UserID, model.TaskRole);
            if (rows == 0)
                return BadRequest();
            return Ok();
        }

        [Route("usersInTask")]
        [HttpGet]
        public async Task<IActionResult> GetUsersInTask([FromQuery] string taskID)
        {
            var users = await _taskRepository.GetUsersInTask(taskID);
            if (users is null || users.Count == 0)
                return BadRequest();
            return Ok(users);
        }

        [Route("tasksForUsers")]
        [HttpGet]
        public async Task<IActionResult> GetTasksForUser([FromQuery] string userID)
        {
            var tasks = await _taskRepository.GetTasksForUser(userID);
            if (tasks is null || tasks.Count == 0)
                return BadRequest();
            return Ok(tasks);
        }

        [Route("removeUserFromTask")]
        [HttpDelete]
        public async Task<IActionResult> RemoveUserFormTask([FromQuery] string taskID, [FromQuery] string userID)
        {
            var tasks = await _taskRepository.RemoveUserFromTask(taskID, userID);
            if (tasks == 0)
                return BadRequest();
            return Ok();
        }
    }
}
