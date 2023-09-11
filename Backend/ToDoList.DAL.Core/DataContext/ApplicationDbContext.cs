using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities.DbSet;
using ToDoList.DAL.Core.Configurations;

namespace ToDoList.DAL.Core.DataContext;

public class ApplicationDbContext : DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TaskConfiguration());
    }
}