using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;
using ToDoList.Application.Exceptions;

namespace ToDoList.Application.Commands.Notes.Delete;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _unitOfWork.Notes.GetNoteByIdAsync(request.NoteId, cancellationToken);

        if (note is null || note.UserId != request.UserId)
            throw new NotFoundException("Note not found!");
        
        await _unitOfWork.Notes.DeleteNoteAsync(request.NoteId, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}