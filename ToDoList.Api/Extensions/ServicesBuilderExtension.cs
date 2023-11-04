using FluentValidation;
using MediatR;
using ToDoList.BL.Services.TaskService;
using ToDoList.DAL.Repositories.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.BL.Queries.TaskQueries;
using ToDoList.DAL.Repositories.TaskRepository;
using ToDoList.Domain.Contracts.Request;
using ToDoList.Domain.Contracts.Response;
using ToDoList.Security.Validators;

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

        return serviceCollection;
    }
}