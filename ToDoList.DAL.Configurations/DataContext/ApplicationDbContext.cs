using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities.DbSet;
using ToDoList.DAL.Configurations.Configurations;

namespace ToDoList.DAL.Configurations.DataContext;

public class ApplicationDbContext : DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; } = null!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
    }
}