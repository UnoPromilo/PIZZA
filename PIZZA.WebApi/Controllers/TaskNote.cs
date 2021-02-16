using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize]
    public class TaskNote : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskNote(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewTaskNoteModel taskNoteModel)
        {
            var rowsAffected = await _taskRepository.AddTaskNote(taskNoteModel);
            if (rowsAffected == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Update([FromBody] TaskNoteModel taskNoteModel)
        {
            var rowsAffected = await _taskRepository.UpdateTaskNote(taskNoteModel);
            if (rowsAffected == 0)
                return BadRequest();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskNote([FromQuery] int taskNote)
        {
            var taskNoteModel = await _taskRepository.GetTaskNote(taskNote.ToString());
            if (taskNoteModel == default)
                return NotFound();
            return Ok(taskNoteModel);
        }

        [HttpDelete]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Remove([FromQuery] int taskNote)
        {
            var rows = await _taskRepository.RemoveTaskNote(taskNote.ToString());
            if (rows == 0)
                return NotFound();
            return Ok();
        }

        [Route("GetNotesForTask")]
        [HttpGet]
        public async Task<IActionResult> GetTaskNotesForTask([FromQuery] int taskID)
        {
            var list = await _taskRepository.GetTaskNotesForTask(taskID.ToString());
            if (list == default)
                return NotFound();
            return Ok(list);
        }
    }
}
