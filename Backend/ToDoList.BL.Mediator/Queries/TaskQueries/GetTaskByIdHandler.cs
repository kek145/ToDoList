namespace ToDoList.BL.Mediator.Queries.TaskQueries;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, GetTaskResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTaskByIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetTaskResponse> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository
            .GetAll()
            .Where(x => x.Id == request.TaskId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        var result = _mapper.Map<GetTaskResponse>(task);

        return result;
    }
}