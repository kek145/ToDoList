using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetData()
        {
            var users = new[]
            {
                new { Name = "Yuri"},
                new { Name = "Janna"},
                new { Name = "Matvei"},
            };
            return Ok(users);
        }
    }
}
