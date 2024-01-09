using System.Threading.Tasks;
using ToDoList.Domain.Request;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Services.NoteService;

public interface INoteService
{
    Task DeleteNoteAsync(int noteId);
    Task CompleteNoteAsync(int noteId);
    Task UpdateNoteAsync(NoteRequest request, int noteId);
    Task<NoteResponse> CreateNoteAsync(NoteRequest request);
}