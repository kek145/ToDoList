using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Identity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        serviceCollection.AddValidatorsFromAssembly(assembly);
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        serviceCollection.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));
        
        return serviceCollection;
    }
}