using MediatR;
using FluentValidation;
using System.Threading.Tasks;
using System.Collections.Generic;
using ToDoList.Security.Exceptions;
using ToDoList.BL.Queries.TaskQueries;
using ToDoList.Domain.Contracts.Request;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Services.TaskService;

public class TaskService : ITaskService
{
    private readonly IMediator _mediator;
    private readonly IValidator<TaskRequestDto> _validator;

    public TaskService(IMediator mediator, IValidator<TaskRequestDto> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }
    public Task<GetTaskResponseDto> CreateTaskAsync(TaskRequestDto request)
    {
        throw new System.NotImplementedException();
    }

    public async Task<GetTaskResponseDto> GetTaskByIdAsync(int taskId)
    {
        var result = await _mediator.Send(new GetTaskByIdQuery(taskId));

        if (result == null)
            throw new NotFoundException("Task not found!!!");

        return result;
    }

    public async Task<IEnumerable<GetTaskResponseDto>> GetAllTaskAsync()
    {
        var result = await _mediator.Send(new GetAllTaskQuery());
        return result;
    }

    public Task UpdateTaskAsync(TaskRequestDto request, int taskId)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteTaskAsync(int taskId)
    {
        throw new System.NotImplementedException();
    }
}