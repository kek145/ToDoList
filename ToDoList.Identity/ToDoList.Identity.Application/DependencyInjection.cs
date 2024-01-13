using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Identity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}