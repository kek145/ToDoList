using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Identity.Application.Services.AuthenticationService;
using ToDoList.Identity.Application.Services.RegistrationService;
using ToDoList.Identity.Application.Services.TokenService;

namespace ToDoList.Identity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        serviceCollection.AddTransient<ITokenService, TokenService>();
        serviceCollection.AddTransient<IRegistrationService, RegistrationService>();
        serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();

        serviceCollection.AddValidatorsFromAssembly(assembly);
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        serviceCollection.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));
        
        return serviceCollection;
    }
}