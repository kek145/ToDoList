using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Queries.Notes.GetAllByPriority;

public class GetAllByPriorityNotesQueryHandler : IRequestHandler<GetAllByPriorityNotesQuery, PagedResult<NoteResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllByPriorityNotesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<NoteResponse>> Handle(GetAllByPriorityNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _unitOfWork.Notes.GetAllByPriorityNotesAsync(request.QueryParameters, request.Priority, request.UserId, cancellationToken);

        var result = _mapper.Map<PagedResult<NoteResponse>>(notes);

        return result;
    }
}