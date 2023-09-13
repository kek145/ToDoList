using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Contracts.Interfaces;
using ToDoList.Domain.Contracts.Response;
using ToDoList.BL.Mediator.Queries.TaskQueries;

namespace ToDoList.BL.Mediator.Handlers.TaskHandlers;

public class GetAllTaskHandler : IRequestHandler<GetAllTaskQuery, IEnumerable<GetTaskResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTaskHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<GetTaskResponse>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _unitOfWork.TaskRepository
            .GetAll()
            .Where(x => x.Status == false)
            .ToListAsync(cancellationToken: cancellationToken);
        var result = _mapper.Map<IEnumerable<GetTaskResponse>>(tasks);

        return result;
    }
}