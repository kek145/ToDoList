using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using ToDoList.Domain.Contracts.Request;
using ToDoList.Domain.Contracts.Response;
using ToDoList.BL.Mediator.Queries.TaskQueries;
using ToDoList.BL.Mediator.Commands.TaskCommands;

namespace ToDoList.BL.Services.TaskService;

public class TaskService : ITaskService
{
    private readonly IMediator _mediator;

    public TaskService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<bool> DeleteTaskAsync(int taskId)
    {
        var command = new DeleteTaskCommand(taskId);

        var result = await _mediator.Send(command);

        if (!result)
            return false;
        
        return true;
    }

    public async Task<GetTaskResponse> GetTaskByIdAsync(int taskId)
    {
        var query = new GetTaskQuery(taskId);

        var result = await _mediator.Send(query);

        return result;
    }

    public async Task<TaskResponse<GetTaskResponse>> GetAllTaskAsync(int page)
    {
        var query = new GetAllTaskQuery(page);

        var result = await _mediator.Send(query);

        return result;
    }

    public async Task<bool> UpdateTaskAsync(TaskRequest request, int taskId)
    {
        var command = new UpdateTaskCommand(taskId, request);

        var result = await _mediator.Send(command);

        if (!result)
            return false;

        return true;
    }

    public async Task<GetTaskResponse> CreateTaskAsync(TaskRequest request)
    {
        var command = new CreateTaskCommand(request);

        var result = await _mediator.Send(command);

        return result;
    }
}