using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.BL.Services.TaskService;
using ToDoList.Domain.Contracts.Request;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("Create-Task")]
    public async Task<IActionResult> CreateTask([FromBody] TaskRequest request)
    {
        var result = await _taskService.CreateTaskAsync(request);

        return StatusCode(201, new { data = result });
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("All-Task")]
    public async Task<IActionResult> GetAllTask([FromQuery] int page = 1)
    {
        var tasks = await _taskService.GetAllTaskAsync(page);

        return Ok(tasks);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("Get-Task/{taskId:int}")]
    public async Task<IActionResult> GetTaskById(int taskId)
    {
        var result = await _taskService.GetTaskByIdAsync(taskId);

        if (result == null!)
            return NotFound(new { error = "Task not found!" });
        
        return Ok(result);
    }

    [HttpPut]
    [AllowAnonymous]
    [Route("Update-Task/{taskId:int}")]
    public async Task<IActionResult> UpdateTask([FromBody] TaskRequest request, int taskId)
    {
        var result = await _taskService.UpdateTaskAsync(request, taskId);

        if (!result)
            return NotFound(new { error = "Task not found!" });

        return NoContent();
    }

    [HttpDelete]
    [AllowAnonymous]
    [Route("Delete-Task/{taskId:int}")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        var result = await _taskService.DeleteTaskAsync(taskId);

        if (!result)
            return NotFound(new { error = "Task not found!" });

        return NoContent();
    }
}