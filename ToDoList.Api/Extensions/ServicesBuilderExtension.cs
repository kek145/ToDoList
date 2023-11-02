using ToDoList.DAL.Repositories.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.DAL.Repositories.TaskRepository;

namespace ToDoList.Api.Extensions;

public static class ServicesBuilderExtension
{
    public static IServiceCollection AddScopedService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<ITaskRepository, TaskRepository>();

        return serviceCollection;
    }
}