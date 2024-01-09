using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Application.Exceptions;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Commands.Notes.Update;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateNoteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _unitOfWork.Notes.GetNoteByIdAsync(request.NoteId, cancellationToken);

        if (note is null || note.UserId != request.UserId)
            throw new NotFoundException("Note not found!");

        note.Title = request.NoteRequest.Title;
        note.Title = request.NoteRequest.Description;
        note.Priority = request.NoteRequest.Priority;
        note.Deadline = request.NoteRequest.Deadline;
        note.UpdatedAt = DateTime.UtcNow;
        
        _unitOfWork.Notes.UpdateNote(note);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}