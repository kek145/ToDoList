using System.Net;
using System.Threading.Tasks;
using ToDoList.Domain.Request;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Services.NoteService;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Helpers;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/notes")]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
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
    public async Task<IActionResult> GetAllTask([FromQuery] QueryParameters queryParameters)
    {
        var response = await _noteService.GetAllNotesAsync(queryParameters);
        return Ok(response);
    }

    [HttpGet]
    [Route("failed")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllFailedTask([FromQuery] QueryParameters queryParameters)
    {
        var response = await _noteService.GetAllFailedNotesAsync(queryParameters);
        return Ok(response);
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
        var response = await _noteService.GetNoteByIdAsync(noteId);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("completed")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllCompletedTask([FromQuery] QueryParameters queryParameters)
    {
        var response = await _noteService.GetAllCompletedNotesAsync(queryParameters);
        return Ok(response);
    }

    [HttpGet]
    [Route("{priority}/priority")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllTaskByPriority([FromQuery] QueryParameters queryParameters, [FromRoute] Priority priority)
    {
        var response = await _noteService.GetAllByPriorityNotesAsync(queryParameters, priority);
        return Ok(response);
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