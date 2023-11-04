using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.BL.Services.TaskService;
using ToDoList.Domain.Contracts.Request;

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
    [Route("create-task")]
    public async Task<IActionResult> CreateTask([FromBody] TaskRequestDto request)
    {
        var result = await _taskService.CreateTaskAsync(request);
        return Ok(result);
    }

    [HttpGet]
    [Route("get-tasks")]
    public async Task<IActionResult> GetAllTasks()
    {
        var result = await _taskService.GetAllTaskAsync();
        return Ok(result);
    }
    
    [HttpGet]
    [Route("get-task/{taskId:int}")]
    public async Task<IActionResult> GetTaskById(int taskId)
    {
        var result = await _taskService.GetTaskByIdAsync(taskId);
        return Ok(result);
    }

    [HttpPut]
    [Route("update-task/{taskId:int}")]
    public async Task<IActionResult> UpdateTask([FromBody] TaskRequestDto request, int taskId)
    {
        await _taskService.UpdateTaskAsync(request, taskId);
        return NoContent();
    }

    [HttpDelete]
    [Route("delete-task/{taskId:int}")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        await _taskService.DeleteTaskAsync(taskId);
        return NoContent();
    }
}