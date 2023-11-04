using Microsoft.Extensions.DependencyInjection;
using ToDoList.DAL.Repositories.TaskRepository;
using ToDoList.DAL.Repositories.UnitOfWork;

namespace ToDoList.Api.Extensions;

public static class ServicesBuilderExtension
{
    public static IServiceCollection AddScopedService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<ITaskRepository, TaskRepository>();

        return serviceCollection;
    }

    // public static IServiceCollection AddTransientService(this IServiceCollection serviceCollection)
    // {
    //     
    // }
}