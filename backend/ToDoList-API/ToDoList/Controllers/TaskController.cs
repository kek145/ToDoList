using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTaskAsync()
        {
            return Ok();
        }

        [HttpGet("GetAllTask")]
        public async Task<IActionResult> GetAllTaskAsync()
        {
            return Ok();
        }

        [HttpPut("UpdateTask/{taskid}")]
        public async Task<IActionResult> UpdateTaskAsync()
        {
            return Ok();
        }

        [HttpDelete("DeleteTask/{taskid}")]
        public async Task<IActionResult> DeleteTaskAsync()
        {
            return Ok();
        }
    }
}
