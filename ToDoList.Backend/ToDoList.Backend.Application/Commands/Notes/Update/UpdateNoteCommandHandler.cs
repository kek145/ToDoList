using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;
using ToDoList.Application.Exceptions;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Helpers;

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
        note.Deadline = request.NoteRequest.Deadline;
        note.UpdatedAt = DateTime.UtcNow;

        note.Priority = request.NoteRequest.Priority switch
        {
            Priority.Easy => PrioritiesHelper.Easy,
            Priority.Medium => PrioritiesHelper.Medium,
            Priority.Hard => PrioritiesHelper.Hard,
            _ => throw new BadRequestException("There is no such priority!")
        };

        _unitOfWork.Notes.UpdateNote(note);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}