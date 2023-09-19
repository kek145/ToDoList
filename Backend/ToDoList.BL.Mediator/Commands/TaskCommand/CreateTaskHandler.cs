namespace ToDoList.BL.Mediator.Commands.TaskCommand;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, GetTaskResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTaskHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetTaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = _mapper.Map<TaskEntity>(request.Request);

        task.UserId = request.UserId;

        await _unitOfWork.TaskRepository.AddAsync(task);
        await _unitOfWork.CommitAsync();

        var result = _mapper.Map<GetTaskResponse>(task);
        
        return result;
    }
}