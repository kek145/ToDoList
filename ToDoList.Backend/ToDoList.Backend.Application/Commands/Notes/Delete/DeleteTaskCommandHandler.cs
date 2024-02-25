using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Commands.Notes.Delete;

public class DeleteTaskCommandHandler(INoteRepository noteRepository) : IRequestHandler<DeleteNoteCommand, long>
{
    private readonly INoteRepository _noteRepository = noteRepository;
    public async Task<long> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var result = await _noteRepository.DeleteNoteAsync(request.NoteId, cancellationToken);
        return result;
    }
}