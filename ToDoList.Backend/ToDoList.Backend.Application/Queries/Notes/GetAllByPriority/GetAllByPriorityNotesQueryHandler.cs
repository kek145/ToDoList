using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Queries.Notes.GetAllByPriority;

public class GetAllByPriorityNotesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllByPriorityNotesQuery, PagedResult<NoteDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<PagedResult<NoteDto>> Handle(GetAllByPriorityNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _unitOfWork.Notes.GetAllByPriorityNotesAsync(request.QueryParameters, request.Priority, request.UserId, cancellationToken);
        return notes;
    }
}