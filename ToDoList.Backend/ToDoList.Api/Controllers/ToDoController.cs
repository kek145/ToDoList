using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/todos")]
public class ToDoController : ControllerBase
{

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateTask()
    {
        return CreatedAtAction(null, new { id = 1 }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTask()
    {
        return Ok();
    }

    [HttpGet]
    [Route("completed")]
    public async Task<IActionResult> GetAllCompletedTask()
    {
        return Ok();
    }

    [HttpGet]
    [Route("failed")]
    public async Task<IActionResult> GetAllFailedTask()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("{taskId:int}")]
    public async Task<IActionResult> GetTaskById([FromRoute] int taskId)
    {
        return Ok();
    }

    [HttpGet]
    [Route("{priority:int}/priority")]
    public async Task<IActionResult> GetAllTaskByPriority([FromRoute] int priority)
    {
        return Ok();
    }

    [HttpPatch]
    [Route("{taskId:int}/complete")]
    public async Task<IActionResult> CompleteTask(int taskId)
    {
        return NoContent();
    }

    [HttpPut]
    [Route("{taskId:int}/update")]
    public async Task<IActionResult> UpdateTask(int taskId)
    {
        return NoContent();
    }
    
    [HttpPut]
    [Route("{taskId:int}/delete")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        return NoContent();
    }
}