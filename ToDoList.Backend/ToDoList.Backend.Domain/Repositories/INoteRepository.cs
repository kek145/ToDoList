using System.Linq;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Response;

namespace ToDoList.Domain.Repositories;

public interface INoteRepository
{
    IQueryable<NoteDto> GetAll();
    Task<long> DeleteNoteAsync(long noteId, CancellationToken cancellationToken = default);
    Task<long> CompleteNoteAsync(long noteId, CancellationToken cancellationToken = default);
    Task<NoteDto?> GetNoteByIdAsync(long noteId, CancellationToken cancellationToken = default);
    Task<NoteDto> AddNoteAsync(NoteDto noteDto, CancellationToken cancellationToken = default);
    Task<long> UpdateNoteAsync(long noteId, NoteDto noteDto, CancellationToken cancellationToken = default);
    Task<PagedResult<NoteDto>> GetAllNotesAsync(QueryParameters queryParameters, int userId, CancellationToken cancellationToken = default);
    Task<PagedResult<NoteDto>> GetAllFailedNotesAsync(QueryParameters queryParameters, int userId, CancellationToken cancellationToken = default);
    Task<PagedResult<NoteDto>> GetAllCompletedNotesAsync(QueryParameters queryParameters, int userId, CancellationToken cancellationToken = default);
    Task<PagedResult<NoteDto>> GetAllByPriorityNotesAsync(QueryParameters queryParameters, string priority, int userId, CancellationToken cancellationToken = default);
}