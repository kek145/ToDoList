using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Domain.Entity;
using ToDoList.Services.Interfaces;
using ToDoList.Services.Models.Dto;
using ToDoList.Services.Validators;

namespace ToDoList.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTaskAsync([FromBody] TaskDto taskDto)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var validator = new DataValidator();
            if (token == null!)
                return BadRequest(new { message = "Null token"});

            if (!validator.TaskValidation(taskDto))
                return BadRequest(new { message = "Task is not valid!"});
            
            await _taskService.CreateTaskAsync(taskDto, token);
            
            return Ok(new { message = "Successful!" });
        }

        [HttpGet("GetAllTask")]
        public async Task<IActionResult> GetAllTaskAsync()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _taskService.GetTasksByUserIdAsync(token);
            if (result == null!)
                return NotFound(new { message = "Tasks is not found!" });
            return Ok(result);
        }

        [HttpPut("UpdateTask/{taskId}")]
        public async Task<IActionResult> UpdateTaskAsync(TaskDto taskDto, int taskId)
        {
            var validator = new DataValidator();
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!validator.TaskValidation(taskDto))
                return BadRequest(new { message = "Fields must be filled!" });
            await _taskService.UpdateTaskAsync(taskDto, token, taskId);
            return Ok(new { message = "Successful!" });
        }

        [HttpDelete("DeleteTask/{taskId}")]
        public async Task<IActionResult> DeleteTaskAsync(int taskId)
        {
            try
            {
                string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                await _taskService.DeleteTaskAsync(taskId, token);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex is Exception)
                    return NotFound(new { message = "Task is not found"});
                if (ex is UnauthorizedAccessException)
                    return Forbid("User is not authorized to update this task");
                return StatusCode(500);
            }
        }
    }
}
