using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PIZZA.DataAccess;
using PIZZA.DataAccess.TaskDatabase;
using PIZZA.Models.Database;
using PIZZA.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebApi.Controllers.Task
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager, Admin")]
    public class CreateTask : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateTask(UserManager<ApplicationUser> userManager, ITaskRepository taskRepository)
        {
            _userManager = userManager;
            _taskRepository = taskRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTaskModel createTaskModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var rowsAffected = await _taskRepository.NewTask(createTaskModel, user.ID);
            if(rowsAffected == 0)
                return BadRequest();
            return Ok();
        }
    }
}
