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
public class NotesController(INoteService noteService) : ControllerBase
{
    private readonly INoteService _noteService = noteService;

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateNote([FromBody] NoteRequest request)
    {
        var response = await _noteService.CreateNoteAsync(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNotes([FromQuery] QueryParameters queryParameters)
    {
        var response = await _noteService.GetAllNotesAsync(queryParameters);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Route("failed")]
    public async Task<IActionResult> GetAllFailedNotes([FromQuery] QueryParameters queryParameters)
    {
        var response = await _noteService.GetAllFailedNotesAsync(queryParameters);
        return StatusCode((int)response.StatusCode, response);
    }
    
    [HttpGet]
    [Route("{noteId:long}")]
    public async Task<IActionResult> GetNoteById([FromRoute] long noteId)
    {
        var response = await _noteService.GetNoteByIdAsync(noteId);
        return StatusCode((int)response.StatusCode, response);
    }
    
    [HttpGet]
    [Route("completed")]
    public async Task<IActionResult> GetAllCompletedNotes([FromQuery] QueryParameters queryParameters)
    {
        var response = await _noteService.GetAllCompletedNotesAsync(queryParameters);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Route("{priority}/priority")]
    public async Task<IActionResult> GetAllNotesByPriority([FromQuery] QueryParameters queryParameters, [FromRoute] Priority priority)
    {
        var response = await _noteService.GetAllByPriorityNotesAsync(queryParameters, priority);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPatch]
    [Route("{noteId:int}/complete")]
    public async Task<IActionResult> CompleteNote(int noteId)
    {
        await _noteService.CompleteNoteAsync(noteId);
        return NoContent();
    }

    [HttpPut]
    [Route("{noteId:long}/update")]
    public async Task<IActionResult> UpdateNote([FromBody] NoteRequest request, [FromRoute] long noteId)
    {
        await _noteService.UpdateNoteAsync(request, noteId);
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{noteId:long}/delete")]
    public async Task<IActionResult> DeleteTask(long noteId)
    {
        await _noteService.DeleteNoteAsync(noteId);
        return NoContent();
    }
}