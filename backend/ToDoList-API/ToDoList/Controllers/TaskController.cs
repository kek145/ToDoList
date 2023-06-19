using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using ToDoList.Services.Interfaces;
using ToDoList.Models.Dto;
using ToDoList.Domain.Entity;

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

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTask(int userid)
        {
            var tasks = await _taskService.GetAllTaskAsync(userid);

            return Ok(tasks);
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> AddNewTask([FromBody] TaskDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskEntity = new TaskEntity
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = taskDto.Status,
                Priority = taskDto.Priority,
                CreatedDate = taskDto.Created,
                UserID = taskDto.UserID 
            };

            await _taskService.CreateTaskAsync(taskEntity);

            return Ok(taskDto);
        }

        [HttpDelete("{taskid}")]
        public async Task<IActionResult> DeleteTask()
        {
            return Ok();
        }

        [HttpPut("{taskid}")]
        public async Task<IActionResult> UpdateTask()
        {
            return Ok();
        }
    }
}
