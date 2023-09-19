namespace ToDoList.BL.Services.TaskService;

public class TaskService : ITaskService
{
    private readonly IMediator _mediator;
    private readonly IValidator<TaskRequest> _validator;

    public TaskService(
        IMediator mediator,
        IValidator<TaskRequest> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<PaginationResponse<GetTaskResponse>> GetAllTasksAsync(int page, int userId)
    {
        var result = await _mediator.Send(new GetAllTaskQuery(page, userId));
        return result;
    }

    public async Task<PaginationResponse<GetTaskResponse>> GetAllFailedTaskAsync(int page, int userId)
    {
        var result = await _mediator.Send(new GetAllFailedTaskQuery(page, userId));
        return result;
    }

    public async Task<PaginationResponse<GetTaskResponse>> GetAllCompletedTaskAsync(int page, int userId)
    {
        var result = await _mediator.Send(new GetAllCompletedTaskQuery(page, userId));
        return result;
    }

    public async Task<PaginationResponse<GetTaskResponse>> SearchTaskAsync(int page, int userId, string search)
    {
        var result = await _mediator.Send(new SearchTaskQuery(page, userId, search));
        return result;
    }

    public async Task<PaginationResponse<GetTaskResponse>> GetAllTasksByPriorityAsync(string priority, int page, int userId)
    {
        var result = await _mediator.Send(new GetAllTaskByPriorityQuery(page, userId, priority));

        if (result == null)
            throw new NotFoundException("Priority not found");

        return result;
    }

    public async Task CompleteTaskAsync(int taskId)
    {
        var result = await _mediator.Send(new CompleteTaskCommand(taskId));

        if (!result)
            throw new NotFoundException("Task not found");
    }

    public async Task<GetTaskResponse> GetTaskByIdAsync(int taskId)
    {
        var result = await _mediator.Send(new GetTaskByIdQuery(taskId));

        if (result == null)
            throw new NotFoundException("Task not found");
        
        return result;
    }

    public async Task UpdateTaskAsync(TaskRequest request, int taskId)
    {
        var result = await _mediator.Send(new UpdateTaskCommand(taskId, request));

        if (!result)
            throw new NotFoundException("Task not found");
    }

    public async Task DeleteTaskAsync(int taskId)
    {
        var result = await _mediator.Send(new DeleteTaskCommand(taskId));

        if (!result)
            throw new NotFoundException("Task not found");
    }

    public async Task<GetTaskResponse> CreateTaskAsync(TaskRequest request, int userId)
    {
        var validator = await _validator.ValidateAsync(request);

        if (!validator.IsValid)
            throw new BadRequestException($"Validation error: {validator}");

        if (request.DeadLine < DateTime.Today || request.DeadLine < DateTime.UtcNow)
            throw new BadRequestException("You cannot specify a date and time lower than the current one");
        
        var result = await _mediator.Send(new CreateTaskCommand(request, userId));

        if (result == null)
            throw new BadRequestException("Task is null!");

        return result;
    }
}