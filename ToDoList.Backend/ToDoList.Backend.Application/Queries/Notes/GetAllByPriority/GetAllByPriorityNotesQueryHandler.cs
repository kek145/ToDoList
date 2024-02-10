using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Interfaces;
using ToDoList.Domain.Result;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Queries.Notes.GetAllByPriority;

public class GetAllByPriorityNotesQueryHandler : IRequestHandler<GetAllByPriorityNotesQuery, PagedResult<NoteDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllByPriorityNotesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PagedResult<NoteDto>> Handle(GetAllByPriorityNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _unitOfWork.Notes.GetAllByPriorityNotesAsync(request.QueryParameters, request.Priority, request.UserId, cancellationToken);
        return notes;
    }
}