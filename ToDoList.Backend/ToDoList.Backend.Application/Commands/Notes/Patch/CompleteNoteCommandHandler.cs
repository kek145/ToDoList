using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Repositories;
using ToDoList.Application.Exceptions;

namespace ToDoList.Application.Commands.Notes.Patch;

public class CompleteNoteCommandHandler(INoteRepository noteRepository) : IRequestHandler<CompleteNoteCommand, long>
{
    private readonly INoteRepository _noteRepository = noteRepository;
    
    public async Task<long> Handle(CompleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetNoteByIdAsync(request.NoteId, cancellationToken);

        if (note is null || note.UserId != request.UserId)
            throw new NotFoundException("Note not found!");

        if (note.Deadline < DateTime.UtcNow)
            throw new BadRequestException("You cannot complete the task because it has already failed!");

        var result = await _noteRepository.CompleteNoteAsync(note.Id, cancellationToken);

        return result;
    }
}