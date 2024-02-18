using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Queries.Notes.GetAllCompleted;

public class GetAllCompletedNotesQueryHandler(INoteRepository noteRepository) : IRequestHandler<GetAllCompletedNotesQuery, PagedResult<NoteDto>>
{
    private readonly INoteRepository _noteRepository = noteRepository;
    
    public async Task<PagedResult<NoteDto>> Handle(GetAllCompletedNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _noteRepository.GetAllCompletedNotesAsync(request.QueryParameters, request.UserId, cancellationToken);
        return notes;
    }
}