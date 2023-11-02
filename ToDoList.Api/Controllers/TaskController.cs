using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{

    [HttpPost]
    [Route("create-task")]
    public async Task<IActionResult> CreateTask()
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