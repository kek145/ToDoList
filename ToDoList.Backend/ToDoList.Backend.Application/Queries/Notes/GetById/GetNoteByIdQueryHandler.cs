using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Repositories;
using ToDoList.Application.Exceptions;

namespace ToDoList.Application.Queries.Notes.GetById;

public class GetNoteByIdQueryHandler(INoteRepository noteRepository) : IRequestHandler<GetNoteByIdQuery, NoteDto>
{
    private readonly INoteRepository _noteRepository = noteRepository;
    
    public async Task<NoteDto> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetNoteByIdAsync(request.NoteId, cancellationToken);

        if (note is null || note.UserId != request.UserId)
            throw new NotFoundException("Примітка не знайдена!");

        return note;
    }
}