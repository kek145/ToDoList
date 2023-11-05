using System;
using MediatR;
using FluentValidation;
using System.Threading.Tasks;
using System.Collections.Generic;
using ToDoList.BL.Commands.TaskCommands;
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
    public async Task<GetTaskResponseDto> CreateTaskAsync(TaskRequestDto request)
    {
        var validator = await _validator.ValidateAsync(request);

        if (!validator.IsValid)
            throw new BadRequestException($"Validation error: {validator}");
        
        if (request.Deadline < DateTime.Today)
            throw new BadRequestException("You cannot specify a date and time lower than the current one");

        var result = await _mediator.Send(new CreateTaskCommand(request));

        return result;
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