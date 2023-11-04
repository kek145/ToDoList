using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.DAL.Repositories.UnitOfWork;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Queries.TaskQueries;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, GetTaskResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTaskByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetTaskResponseDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var search = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);

        var result = _mapper.Map<GetTaskResponseDto>(search);

        return result;
    }
}