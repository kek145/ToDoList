using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTask()
        {
            return Ok();
        }

        [HttpDelete("{taskid}")]
        public async Task<IActionResult> DeleteTask()
        {
            return Ok();
        }

        [HttpPatch("{taskid}")]
        public async Task<IActionResult> UpdateTask()
        {
            return Ok();
        }
    }
}
