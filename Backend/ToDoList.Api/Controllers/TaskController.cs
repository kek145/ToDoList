namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/task")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateTask([FromBody] TaskRequest request)
    {
        var response = await _taskService.CreateTaskAsync(request);
        return StatusCode(201, response);
    }

    [HttpGet]
    [Route("tasks")]
    public async Task<IActionResult> GetAllTasks([FromQuery] int page = 1)
    {
        var response = await _taskService.GetAllTasksAsync(page);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("failed")]
    public async Task<IActionResult> GetAllFailedTasks([FromQuery] int page = 1)
    {
        var response = await _taskService.GetAllFailedTaskAsync(page);
        return Ok(response);
    }

    [HttpGet]
    [Route("completed")]
    public async Task<IActionResult> GetAllCompleteTasks([FromQuery] int page = 1)
    {
        var response = await _taskService.GetAllCompletedTaskAsync(page);
        return Ok(response);
    }

    [HttpGet]
    [Route("{taskId:int}")]
    public async Task<IActionResult> GetTaskById(int taskId)
    {
        var response = await _taskService.GetTaskByIdAsync(taskId);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("by-priority")]
    public async Task<IActionResult> GetAllTasksByPriority([FromQuery] string priority, [FromQuery] int page = 1)
    {
        var response = await _taskService.GetAllTasksByPriorityAsync(priority, page);
        return Ok(response);
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> SearchTask([FromQuery] string search, [FromQuery] int page = 1)
    {
        var response = await _taskService.SearchTaskAsync(page,search);
        return Ok(response);
    }

    [HttpPatch]
    [Route("complete/{taskId:int}")]
    public async Task<IActionResult> CompleteTask(int taskId)
    {
        await _taskService.CompleteTaskAsync(taskId);
        return NoContent();
    }

    [HttpPut]
    [Route("update/{taskId:int}")]
    public async Task<IActionResult> UpdateTask([FromBody] TaskRequest request, int taskId)
    {
        await _taskService.UpdateTaskAsync(request, taskId);
        return NoContent();
    }

    [HttpDelete]
    [Route("delete/{taskId:int}")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        await _taskService.DeleteTaskAsync(taskId);
        return NoContent();
    }
}