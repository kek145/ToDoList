using System.Linq;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Queries.Notes.GetAll;

public class GetAllNotesQueryHandler : IRequestHandler<GetAllNotesQuery ,PagedResult<NoteResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllNotesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<NoteResponse>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _unitOfWork.Notes.GetAllNotesAsync(request.QueryParameters, request.UserId, cancellationToken);

        var result = _mapper.Map<PagedResult<NoteResponse>>(notes);

        return result;
    }
}