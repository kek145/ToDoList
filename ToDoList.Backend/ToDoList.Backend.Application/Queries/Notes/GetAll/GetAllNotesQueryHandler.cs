using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Result;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Queries.Notes.GetAll;

public class GetAllNotesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllNotesQuery ,PagedResult<NoteDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PagedResult<NoteDto>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _unitOfWork.Notes.GetAllNotesAsync(request.QueryParameters, request.UserId, cancellationToken);
        return notes;
    }
}