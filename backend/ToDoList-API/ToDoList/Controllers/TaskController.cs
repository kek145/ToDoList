using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Domain.Entity;
using ToDoList.Services.Interfaces;
using ToDoList.Services.Validators;

namespace ToDoList.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTaskAsync(TaskEntity entity)
        {
            var validator = new DataValidator();
            if (!validator.TaskValidation(entity))
                return BadRequest("Fields must be filled!");
            
            await _taskService.CreateTaskAsync(entity);
            return Ok("Successful!");
        }

        [HttpGet("GetAllTask")]
        public async Task<IActionResult> GetAllTaskAsync(int userId)
        {
            var result = await _taskService.GetTasksByUserIdAsync(userId);
            if (result is null)
                return NotFound("Tasks is not found!");
            return Ok(result);
        }

        [HttpPut("UpdateTask/{taskId}")]
        public async Task<IActionResult> UpdateTaskAsync(TaskEntity entity, int taskId)
        {
            var validator = new DataValidator();
            if (!validator.TaskValidation(entity))
                return BadRequest("Fields must be filled!");
            await _taskService.UpdateTaskAsync(entity, taskId);
            return Ok("Successful!");
        }

        [HttpDelete("DeleteTask/{taskId}")]
        public async Task<IActionResult> DeleteTaskAsync(int taskId, int userId)
        {
            var task = await _taskService.GetTasksByUserIdAsync(userId);
            if (task is null)
                return NotFound("No such task!");
            await _taskService.DeleteTaskAsync(taskId, userId);
            return Ok("Successful!");
        }
    }
}
