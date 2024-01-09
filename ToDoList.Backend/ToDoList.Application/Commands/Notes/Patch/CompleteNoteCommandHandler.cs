using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;
using ToDoList.Application.Exceptions;

namespace ToDoList.Application.Commands.Notes.Patch;

public class CompleteNoteCommandHandler : IRequestHandler<CompleteNoteCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CompleteNoteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(CompleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _unitOfWork.Notes.GetNoteByIdAsync(request.NoteId, cancellationToken);

        if (note is null || note.UserId != request.UserId)
            throw new NotFoundException("Note not found!");

        note.Status = true;

        _unitOfWork.Notes.UpdateNote(note);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}