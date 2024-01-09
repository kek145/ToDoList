using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ToDoList.Domain.Interfaces;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Queries.Notes.GetAllFailed;

public class GetAllFailedNotesQueryHandler : IRequestHandler<GetAllFailedNotesQuery, PagedResult<NoteResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllFailedNotesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<PagedResult<NoteResponse>> Handle(GetAllFailedNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _unitOfWork.Notes.GetAllFailedNotesAsync(request.QueryParameters, request.UserId, cancellationToken);

        var result = _mapper.Map<PagedResult<NoteResponse>>(notes);

        return result;
    }
}