using System.Net;
using System.Threading.Tasks;
using ToDoList.Domain.Request;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Services.NoteService;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/todos")]
public class ToDoController : ControllerBase
{
    private readonly INoteService _noteService;

    public ToDoController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> CreateTask([FromBody] NoteRequest request)
    {
        var response = await _noteService.CreateNoteAsync(request);
        return CreatedAtAction(null, null, response);
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllTask()
    {
        return Ok();
    }

    [HttpGet]
    [Route("completed")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllCompletedTask()
    {
        return Ok();
    }

    [HttpGet]
    [Route("failed")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllFailedTask()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("{taskId:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetTaskById([FromRoute] int taskId)
    {
        return Ok();
    }

    [HttpGet]
    [Route("{priority:int}/priority")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllTaskByPriority([FromRoute] int priority)
    {
        return Ok();
    }

    [HttpPatch]
    [Route("{taskId:int}/complete")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> CompleteTask(int taskId)
    {
        return NoContent();
    }

    [HttpPut]
    [Route("{taskId:int}/update")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> UpdateTask(int taskId)
    {
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{taskId:int}/delete")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        return NoContent();
    }
}