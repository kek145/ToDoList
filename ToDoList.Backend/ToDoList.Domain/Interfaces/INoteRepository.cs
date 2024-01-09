using System.Linq;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Result;

namespace ToDoList.Domain.Interfaces;

public interface INoteRepository
{
    IQueryable<NoteDto> GetAll();
    void UpdateNote(NoteDto noteDto);
    Task DeleteNoteAsync(int noteId, CancellationToken cancellationToken = default);
    Task<NoteDto?> GetNoteByIdAsync(int noteId, CancellationToken cancellationToken = default);
    Task<NoteDto> AddNoteAsync(NoteDto noteDto, CancellationToken cancellationToken = default);
    Task<PagedResult<NoteDto>> GetAllNotesAsync(QueryParameters queryParameters, int userId, CancellationToken cancellationToken = default);
    Task<PagedResult<NoteDto>> GetAllFailedNotesAsync(QueryParameters queryParameters, int userId, CancellationToken cancellationToken = default);
    Task<PagedResult<NoteDto>> GetAllCompletedNotesAsync(QueryParameters queryParameters, int userId, CancellationToken cancellationToken = default);
    Task<PagedResult<NoteDto>> GetAllByPriorityNotesAsync(QueryParameters queryParameters, string priority, int userId, CancellationToken cancellationToken = default);
}