using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoList.Identity.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Identity.Infrastructure.DataStore;
using ToDoList.Identity.Infrastructure.Repositories;
using ToDoList.Identity.Infrastructure.Implementations;

namespace ToDoList.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention();
        });
        
        return serviceCollection;
    }
}