using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;
using ToDoList.Application.Exceptions;

namespace ToDoList.Application.Queries.Notes.GetById;

public class GetNoteByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetNoteByIdQuery, NoteDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<NoteDto> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var note = await _unitOfWork.Notes.GetNoteByIdAsync(request.NoteId, cancellationToken);

        if (note is null || note.UserId != request.UserId)
            throw new NotFoundException("Note not found!");

        return note;
    }
}