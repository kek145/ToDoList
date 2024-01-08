using System.Linq;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;

namespace ToDoList.Domain.Interfaces;

public interface INoteRepository
{
    IQueryable<NoteDto> GetAll();
    void UpdateNote(NoteDto noteDto);
    Task DeleteNoteAsync(int noteId, CancellationToken cancellationToken = default);
    Task<NoteDto?> GetNoteByIdAsync(int taskId, CancellationToken cancellationToken = default);
    Task<NoteDto> AddNoteAsync(NoteDto noteDto, CancellationToken cancellationToken = default);
}