﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Identity.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return serviceCollection;
    }
}