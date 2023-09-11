using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{


    [HttpPost]
    [AllowAnonymous]
    [Route("Create-Task")]
    public async Task<IActionResult> CreateTask()
    {
        return Ok("asd");
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("All-Task")]
    public async Task<IActionResult> GetAllTask()
    {
        return Ok();
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("Get-Task/{taskId:int}")]
    public async Task<IActionResult> GetTaskById(int taskId)
    {
        return Ok();
    }

    [HttpPut]
    [AllowAnonymous]
    [Route("Update-Task/{taskId:int}")]
    public async Task<IActionResult> UpdateTask(int taskId)
    {
        return Ok();
    }

    [HttpDelete]
    [AllowAnonymous]
    [Route("Delete-Task/{taskId:int}")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        return NoContent();
    }
}