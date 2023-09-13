using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Contracts.Response;
using ToDoList.BL.Mediator.Queries.TaskQueries;
using ToDoList.DAL.Contracts.Interfaces;

namespace ToDoList.BL.Mediator.Handlers.TaskHandlers;

public class GetTaskHandler : IRequestHandler<GetTaskQuery, GetTaskResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTaskHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetTaskResponse> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository
            .GetAll()
            .Where(find => find.Id == request.TaskId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        var result = _mapper.Map<GetTaskResponse>(task);

        return result;
    }
}