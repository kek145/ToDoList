using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Services.Interfaces;
using Microsoft.Extensions.Logging;

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
    }
}
