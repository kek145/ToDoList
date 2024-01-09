using ToDoList.Domain.Enum;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Request;

namespace ToDoList.Application.Services.NoteService;

public interface INoteService
{
    Task DeleteNoteAsync(int noteId);
    Task CompleteNoteAsync(int noteId);
    Task<NoteResponse> GetNoteByIdAsync(int noteId);
    Task UpdateNoteAsync(NoteRequest request, int noteId);
    Task<NoteResponse> CreateNoteAsync(NoteRequest request);
    Task<PagedResult<NoteResponse>> GetAllNotesAsync(QueryParameters queryParameters);
    Task<PagedResult<NoteResponse>> GetAllFailedNotesAsync(QueryParameters queryParameters);
    Task<PagedResult<NoteResponse>> GetAllCompletedNotesAsync(QueryParameters queryParameters);
    Task<PagedResult<NoteResponse>> GetAllByPriorityNotesAsync(QueryParameters queryParameters, Priority priority);
}