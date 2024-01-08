using ToDoList.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure.DataStore;
using Microsoft.Extensions.Configuration;
using ToDoList.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<INoteRepository, NoteRepository>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        
        serviceCollection.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention();
        });
        
        return serviceCollection;
    }
}