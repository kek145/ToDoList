using System.Net;
using ToDoList.Domain.Enum;
using System.Threading.Tasks;
using ToDoList.Domain.Request;
using ToDoList.Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Application.Services.NoteService;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/notes")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    public async Task<IActionResult> CreateNote([FromBody] NoteRequest request)
    {
        var response = await _noteService.CreateNoteAsync(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllNotes([FromQuery] QueryParameters queryParameters)
    {
        var response = await _noteService.GetAllNotesAsync(queryParameters);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Route("failed")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllFailedNotes([FromQuery] QueryParameters queryParameters)
    {
        var response = await _noteService.GetAllFailedNotesAsync(queryParameters);
        return StatusCode((int)response.StatusCode, response);
    }
    
    [HttpGet]
    [Route("{noteId:long}")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetNoteById([FromRoute] long noteId)
    {
        var response = await _noteService.GetNoteByIdAsync(noteId);
        return StatusCode((int)response.StatusCode, response);
    }
    
    [HttpGet]
    [Route("completed")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllCompletedNotes([FromQuery] QueryParameters queryParameters)
    {
        var response = await _noteService.GetAllCompletedNotesAsync(queryParameters);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Route("{priority}")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllNotesByPriority([FromQuery] QueryParameters queryParameters, [FromRoute] Priority priority)
    {
        var response = await _noteService.GetAllByPriorityNotesAsync(queryParameters, priority);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPatch]
    [Route("{noteId:int}/complete")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> CompleteNote(int noteId)
    {
        await _noteService.CompleteNoteAsync(noteId);
        return NoContent();
    }

    [HttpPut]
    [Route("{noteId:long}/update")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> UpdateNote([FromBody] NoteRequest request, [FromRoute] long noteId)
    {
        await _noteService.UpdateNoteAsync(request, noteId);
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{noteId:long}/delete")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteTask(long noteId)
    {
        await _noteService.DeleteNoteAsync(noteId);
        return NoContent();
    }
}