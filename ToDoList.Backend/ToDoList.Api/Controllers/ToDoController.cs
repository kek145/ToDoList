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
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> CreateTask([FromBody] NoteRequest request)
    {
        var response = await _noteService.CreateNoteAsync(request);
        return CreatedAtAction(null, null, response);
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllTask()
    {
        return Ok();
    }

    [HttpGet]
    [Route("completed")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllCompletedTask()
    {
        return Ok();
    }

    [HttpGet]
    [Route("failed")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllFailedTask()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("{noteId:int}")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetTaskById([FromRoute] int noteId)
    {
        return Ok();
    }

    [HttpGet]
    [Route("{priority:int}/priority")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllTaskByPriority([FromRoute] int priority)
    {
        return Ok();
    }

    [HttpPatch]
    [Route("{noteId:int}/complete")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> CompleteTask(int noteId)
    {
        await _noteService.CompleteNoteAsync(noteId);
        return NoContent();
    }

    [HttpPut]
    [Route("{noteId:int}/update")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> UpdateTask([FromBody] NoteRequest request, [FromRoute] int noteId)
    {
        await _noteService.UpdateNoteAsync(request, noteId);
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{noteId:int}/delete")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteTask(int noteId)
    {
        await _noteService.DeleteNoteAsync(noteId);
        return NoContent();
    }
}