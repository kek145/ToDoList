using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Result;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Queries.Notes.GetAllFailed;

public class GetAllFailedNotesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllFailedNotesQuery, PagedResult<NoteDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<PagedResult<NoteDto>> Handle(GetAllFailedNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _unitOfWork.Notes.GetAllFailedNotesAsync(request.QueryParameters, request.UserId, cancellationToken);
        return notes;
    }
}