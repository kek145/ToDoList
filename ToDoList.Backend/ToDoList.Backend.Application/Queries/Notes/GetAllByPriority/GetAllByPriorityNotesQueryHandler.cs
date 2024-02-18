using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Queries.Notes.GetAllByPriority;

public class GetAllByPriorityNotesQueryHandler(INoteRepository noteRepository) : IRequestHandler<GetAllByPriorityNotesQuery, PagedResult<NoteDto>>
{
    private readonly INoteRepository _noteRepository = noteRepository;
    
    public async Task<PagedResult<NoteDto>> Handle(GetAllByPriorityNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _noteRepository.GetAllByPriorityNotesAsync(request.QueryParameters, request.Priority, request.UserId, cancellationToken);
        return notes;
    }
}