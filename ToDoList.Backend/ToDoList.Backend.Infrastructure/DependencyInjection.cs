using ToDoList.Domain.Interfaces;
using ToDoList.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure.DataStore;
using Microsoft.Extensions.Configuration;
using ToDoList.Infrastructure.Repositories;
using ToDoList.Infrastructure.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<INoteRepository, NoteRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention();
        });
        
        return serviceCollection;
    }
}