using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Response;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Queries.Notes.GetAllFailed;

public class GetAllFailedNotesQueryHandler(INoteRepository noteRepository) : IRequestHandler<GetAllFailedNotesQuery, PagedResult<NoteDto>>
{
    private readonly INoteRepository _noteRepository = noteRepository;
    public async Task<PagedResult<NoteDto>> Handle(GetAllFailedNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _noteRepository.GetAllFailedNotesAsync(request.QueryParameters, request.UserId, cancellationToken);
        return notes;
    }
}