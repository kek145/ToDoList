using System.Collections.Generic;
using MediatR;
using FluentValidation;
using ToDoList.Security.Validators;
using ToDoList.BL.Queries.TaskQueries;
using ToDoList.BL.Services.TaskService;
using ToDoList.Domain.Contracts.Request;
using ToDoList.Domain.Contracts.Response;
using ToDoList.DAL.Repositories.UnitOfWork;
using ToDoList.DAL.Repositories.TaskRepository;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Api.Extensions;

public static class ServicesBuilderExtension
{
    public static IServiceCollection AddScopedService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<ITaskRepository, TaskRepository>();

        return serviceCollection;
    }

    public static IServiceCollection AddTransientService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ITaskService, TaskService>();
        serviceCollection.AddTransient<IValidator<TaskRequestDto>, TaskRequestValidator>();
        serviceCollection.AddTransient<IRequestHandler<GetTaskByIdQuery, GetTaskResponseDto>, GetTaskByIdQueryHandler>();
        serviceCollection.AddTransient<IRequestHandler<GetAllTaskQuery, IEnumerable<GetTaskResponseDto>>, GetAllTaskQueryHandler>();

        return serviceCollection;
    }
}