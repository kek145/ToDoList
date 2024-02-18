using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Result;
using System.Threading.Tasks;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Queries.Notes.GetAll;

public class GetAllNotesQueryHandler(INoteRepository noteRepository) : IRequestHandler<GetAllNotesQuery ,PagedResult<NoteDto>>
{
    private readonly INoteRepository _noteRepository = noteRepository;

    public async Task<PagedResult<NoteDto>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _noteRepository.GetAllNotesAsync(request.QueryParameters, request.UserId, cancellationToken);
        return notes;
    }
}