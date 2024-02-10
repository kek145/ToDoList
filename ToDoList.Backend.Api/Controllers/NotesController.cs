using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Backend.Api.Controllers;

[ApiController]
[Route("api/notes")]
public class NotesController : ControllerBase
{
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateNote()
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNotes()
    {
        return Ok();
    }

    [HttpGet]
    [Route("failed")]
    public async Task<IActionResult> GetAllFailedNotes()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("{noteId:long}")]
    public async Task<IActionResult> GetNoteById([FromRoute] long noteId)
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("completed")]
    public async Task<IActionResult> GetAllCompletedNotes()
    {
        return Ok();
    }

    [HttpGet]
    [Route("{priority}")]
    public async Task<IActionResult> GetAllNotesByPriority()
    {
        return Ok();
    }

    [HttpPatch]
    [Route("{noteId:int}/complete")]
    public async Task<IActionResult> CompleteNote(int noteId)
    {
        return NoContent();
    }

    [HttpPut]
    [Route("{noteId:long}/update")]
    public async Task<IActionResult> UpdateNote(long noteId)
    {
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{noteId:long}/delete")]
    public async Task<IActionResult> DeleteTask(long noteId)
    {
        return NoContent();
    }
}