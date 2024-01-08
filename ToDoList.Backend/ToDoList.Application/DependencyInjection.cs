using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Services.NoteService;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        serviceCollection.AddValidatorsFromAssembly(assembly);
        serviceCollection.AddTransient<INoteService, NoteService>();
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        serviceCollection.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));
        
        return serviceCollection;
    }
}