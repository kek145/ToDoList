using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Queries.Notes.GetAllCompleted;

public class GetAllCompletedNotesQueryHandler : IRequestHandler<GetAllCompletedNotesQuery, PagedResult<NoteResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCompletedNotesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<PagedResult<NoteResponse>> Handle(GetAllCompletedNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _unitOfWork.Notes.GetAllCompletedNotesAsync(request.QueryParameters, request.UserId, cancellationToken);

        var result = _mapper.Map<PagedResult<NoteResponse>>(notes);

        return result;
    }
}