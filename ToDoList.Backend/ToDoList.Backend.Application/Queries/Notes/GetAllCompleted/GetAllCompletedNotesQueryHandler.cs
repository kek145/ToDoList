using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Queries.Notes.GetAllCompleted;

public class GetAllCompletedNotesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCompletedNotesQuery, PagedResult<NoteDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<PagedResult<NoteDto>> Handle(GetAllCompletedNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _unitOfWork.Notes.GetAllCompletedNotesAsync(request.QueryParameters, request.UserId, cancellationToken);
        return notes;
    }
}