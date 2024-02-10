using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Services.NoteService;
using ToDoList.Application.Services.TokenService;
using ToDoList.Application.Services.AuthenticationService;
using ToDoList.Application.Services.RegistrationService;

namespace ToDoList.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        serviceCollection.AddValidatorsFromAssembly(assembly);
        
        serviceCollection.AddTransient<INoteService, NoteService>();
        serviceCollection.AddTransient<ITokenService, TokenService>();
        serviceCollection.AddTransient<IRegistrationService, RegistrationService>();
        serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
        
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        serviceCollection.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));
        
        return serviceCollection;
    }
}