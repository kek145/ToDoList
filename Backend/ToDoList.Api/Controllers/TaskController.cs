namespace ToDoList.Api.Controllers;

[ApiController]
[Route("todo/api/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskRequest request)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });
        
        var response = await _taskService.CreateTaskAsync(request, Convert.ToInt32(userId));

        return StatusCode(201, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks([FromQuery] int page = 1)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });
        
        var response = await _taskService.GetAllTasksAsync(page,Convert.ToInt32(userId));

        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllFailedTasks([FromQuery] int page = 1)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });

        var response = await _taskService.GetAllFailedTaskAsync(page, Convert.ToInt32(userId));
        
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompleteTasks([FromQuery] int page = 1)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });

        var response = await _taskService.GetAllCompletedTaskAsync(page, Convert.ToInt32(userId));
        
        return Ok(response);
    }

    [HttpGet]
    [Route("task{taskId:int}")]
    public async Task<IActionResult> GetTaskById(int taskId)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });
        
        var response = await _taskService.GetTaskByIdAsync(taskId);
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTasksByPriority([FromQuery] string priority, [FromQuery] int page = 1)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });

        var response = await _taskService.GetAllTasksByPriorityAsync(priority, page,Convert.ToInt32(userId));
        
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> SearchTask([FromQuery] string search, [FromQuery] int page = 1)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });
        
        var response = await _taskService.SearchTaskAsync(page, Convert.ToInt32(userId), search);
        return Ok(response);
    }

    [HttpPatch]
    [Route("task{taskId:int}")]
    public async Task<IActionResult> CompleteTask(int taskId)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });
        
        await _taskService.CompleteTaskAsync(taskId);
        return NoContent();
    }

    [HttpPut]
    [Route("task{taskId:int}")]
    public async Task<IActionResult> UpdateTask([FromBody] TaskRequest request, int taskId)
    {
        var userId = User.FindFirst("UserId")?.Value;
        
        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });

        await _taskService.UpdateTaskAsync(request, taskId);
        
        return NoContent();
    }

    [HttpDelete]
    [Route("task{taskId:int}")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        var userId = User.FindFirst("UserId")?.Value;
        
        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });

        await _taskService.DeleteTaskAsync(taskId);
        
        return NoContent();
    }
}