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
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequestDto request)
    {
        return Ok();
    }

    [HttpGet]
    [Route("getTaskById/{taskId:int}")]
    public async Task<IActionResult> GetTaskById(int taskId)
    {
        return Ok();
    }

    [HttpGet]
    [Route("allTasks")]
    public async Task<IActionResult> GetAllTasks()
    {
        return Ok();
    }

    [HttpPut]
    [Route("update-task/{taskId:int}")]
    public async Task<IActionResult> UpdateTask(int taskId)
    {
        return NoContent();
    }

    [HttpDelete]
    [Route("delete-task/{taskId}")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        return NoContent();
    }
}