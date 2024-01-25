using ToDoList.Domain.Enum;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Interfaces;
using ToDoList.Domain.Request;

namespace ToDoList.Application.Services.NoteService;

public interface INoteService
{
    Task DeleteNoteAsync(int noteId);
    Task CompleteNoteAsync(int noteId);
    Task<IBaseResponse<NoteResponse>> GetNoteByIdAsync(int noteId);
    Task UpdateNoteAsync(NoteRequest request, int noteId);
    Task<IBaseResponse<NoteResponse>> CreateNoteAsync(NoteRequest request);
    Task<IBaseResponse<PagedResult<NoteResponse>>> GetAllNotesAsync(QueryParameters queryParameters);
    Task<IBaseResponse<PagedResult<NoteResponse>>> GetAllFailedNotesAsync(QueryParameters queryParameters);
    Task<IBaseResponse<PagedResult<NoteResponse>>> GetAllCompletedNotesAsync(QueryParameters queryParameters);
    Task<IBaseResponse<PagedResult<NoteResponse>>> GetAllByPriorityNotesAsync(QueryParameters queryParameters, Priority priority);
}