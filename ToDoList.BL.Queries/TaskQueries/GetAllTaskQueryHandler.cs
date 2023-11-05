using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ToDoList.Domain.Contracts.Response;
using ToDoList.DAL.Repositories.UnitOfWork;

namespace ToDoList.BL.Queries.TaskQueries;

public class GetAllTaskQueryHandler : IRequestHandler<GetAllTaskQuery, IEnumerable<GetTaskResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTaskQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<GetTaskResponseDto>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _unitOfWork.TaskRepository.GetAllAsync();

        var result = _mapper.Map<IEnumerable<GetTaskResponseDto>>(tasks);

        return result;
    }
}