using Microsoft.AspNetCore.Http;

namespace ToDoList.BL.Services.TaskService;

public class TaskService : ITaskService
{
    private string _userId = string.Empty;
    private readonly IMediator _mediator;
    private readonly IValidator<TaskRequest> _validator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TaskService(
        IMediator mediator,
        IValidator<TaskRequest> validator,
        IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _validator = validator;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PaginationResponse<GetTaskResponse>> GetAllTasksAsync(int page)
    {
        if (_httpContextAccessor.HttpContext is not null)
            _userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId")!.Value;
        else
            throw new UnauthorizedException("User is not found");
        
        var result = await _mediator.Send(new GetAllTaskQuery(page, Convert.ToInt32(_userId)));
        return result;
    }

    public async Task<PaginationResponse<GetTaskResponse>> GetAllFailedTaskAsync(int page)
    {
        if (_httpContextAccessor.HttpContext is not null)
            _userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId")!.Value;
        else
            throw new UnauthorizedException("User is not found");
        
        var result = await _mediator.Send(new GetAllFailedTaskQuery(page, Convert.ToInt32(_userId)));
        return result;
    }

    public async Task<PaginationResponse<GetTaskResponse>> GetAllCompletedTaskAsync(int page)
    {
        if (_httpContextAccessor.HttpContext is not null)
            _userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId")!.Value;
        else
            throw new UnauthorizedException("User is not found");
        
        var result = await _mediator.Send(new GetAllCompletedTaskQuery(page, Convert.ToInt32(_userId)));
        return result;
    }

    public async Task<PaginationResponse<GetTaskResponse>> SearchTaskAsync(int page, string search)
    {
        if (_httpContextAccessor.HttpContext is not null)
            _userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId")!.Value;
        else
            throw new UnauthorizedException("User is not found");
        
        var result = await _mediator.Send(new SearchTaskQuery(page, Convert.ToInt32(_userId), search));
        return result;
    }

    public async Task<PaginationResponse<GetTaskResponse>> GetAllTasksByPriorityAsync(string priority, int page)
    {
        if (_httpContextAccessor.HttpContext is not null)
            _userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId")!.Value;
        else
            throw new UnauthorizedException("User is not found");
        
        var result = await _mediator.Send(new GetAllTaskByPriorityQuery(page, Convert.ToInt32(_userId), priority));

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

    public async Task<GetTaskResponse> CreateTaskAsync(TaskRequest request)
    {
        if (_httpContextAccessor.HttpContext is not null)
            _userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId")!.Value;
        else
            throw new UnauthorizedException("User is not found");
        
        var validator = await _validator.ValidateAsync(request);

        if (!validator.IsValid)
            throw new BadRequestException($"Validation error: {validator}");

        if (request.DeadLine < DateTime.Today)
            throw new BadRequestException("You cannot specify a date and time lower than the current one");
        
        var result = await _mediator.Send(new CreateTaskCommand(request, Convert.ToInt32(_userId)));

        if (result == null)
            throw new BadRequestException("Task is null!");

        return result;
    }
}