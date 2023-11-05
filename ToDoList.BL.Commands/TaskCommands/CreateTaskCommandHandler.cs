using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Contracts.Response;
using ToDoList.DAL.Repositories.UnitOfWork;
using ToDoList.Domain.Entities.DbSet;

namespace ToDoList.BL.Commands.TaskCommands;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, GetTaskResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTaskCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetTaskResponseDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = _mapper.Map<TaskEntity>(request.Request);

        await _unitOfWork.TaskRepository.AddAsync(task);
        await _unitOfWork.CommitAsync();

        var result = _mapper.Map<GetTaskResponseDto>(task);

        return result;
    }
}