using System.Threading.Tasks;
using ToDoList.Domain.Request;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Services.NoteService;

public interface INoteService
{
    Task<NoteResponse> CreateNoteAsync(NoteRequest request);
}