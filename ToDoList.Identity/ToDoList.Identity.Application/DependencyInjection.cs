using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Identity.Application.Services.RegistrationService;

namespace ToDoList.Identity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        serviceCollection.AddTransient<IRegistrationService, RegistrationService>();

        serviceCollection.AddValidatorsFromAssembly(assembly);
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        serviceCollection.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));
        
        return serviceCollection;
    }
}