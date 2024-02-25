using System.Net;
using ToDoList.Domain.Enum;
using System.Threading.Tasks;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Request;
using ToDoList.Domain.Response;
using ToDoList.Domain.Abstractions;

namespace ToDoList.Application.Services.NoteService;

public interface INoteService
{
    Task<HttpStatusCode> DeleteNoteAsync(long noteId);
    Task<HttpStatusCode> CompleteNoteAsync(long noteId);
    Task<IBaseResponse<NoteResponse>> GetNoteByIdAsync(long noteId);
    Task<HttpStatusCode> UpdateNoteAsync(NoteRequest request, long noteId);
    Task<IBaseResponse<NoteResponse>> CreateNoteAsync(NoteRequest request);
    Task<IBaseResponse<PagedResult<NoteResponse>>> GetAllNotesAsync(QueryParameters queryParameters);
    Task<IBaseResponse<PagedResult<NoteResponse>>> GetAllFailedNotesAsync(QueryParameters queryParameters);
    Task<IBaseResponse<PagedResult<NoteResponse>>> GetAllCompletedNotesAsync(QueryParameters queryParameters);
    Task<IBaseResponse<PagedResult<NoteResponse>>> GetAllByPriorityNotesAsync(QueryParameters queryParameters, Priority priority);
}