using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.BL.Mediator.Commands.TaskCommands;
using ToDoList.DAL.Contracts.Interfaces;
using ToDoList.Domain.Contracts.Response;
using ToDoList.Domain.Entities.DbSet;

namespace ToDoList.BL.Mediator.Handlers.TaskHandlers;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, GetTaskResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTaskHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetTaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = _mapper.Map<TaskEntity>(request.Request);

        await _unitOfWork.TaskRepository.CreateAsync(task);
        await _unitOfWork.CommitAsync();

        var result = _mapper.Map<GetTaskResponse>(task);
        return result;
    }
}