using Microsoft.EntityFrameworkCore;
using ToDoList.Identity.Domain.DbSet;
using ToDoList.Identity.Infrastructure.Configurations;

namespace ToDoList.Identity.Infrastructure.DataStore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}