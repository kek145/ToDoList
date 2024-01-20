using System.Net;
using ToDoList.Domain.Enum;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Domain.Request;
using ToDoList.Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Services.NoteService;

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
        return CreatedAtAction(null, null, response);
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllNotes([FromQuery] QueryParameters queryParameters)
    {
        var response = await _noteService.GetAllNotesAsync(queryParameters);
        return Ok(response);
    }

    [HttpGet]
    [Route("failed")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllFailedNotes([FromQuery] QueryParameters queryParameters)
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
    public async Task<IActionResult> GetNoteById([FromRoute] int noteId)
    {
        var response = await _noteService.GetNoteByIdAsync(noteId);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("completed")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAllCompletedNotes([FromQuery] QueryParameters queryParameters)
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
    public async Task<IActionResult> GetAllNotesByPriority([FromQuery] QueryParameters queryParameters, [FromRoute] Priority priority)
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
    public async Task<IActionResult> CompleteNote(int noteId)
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
    public async Task<IActionResult> UpdateNote([FromBody] NoteRequest request, [FromRoute] int noteId)
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