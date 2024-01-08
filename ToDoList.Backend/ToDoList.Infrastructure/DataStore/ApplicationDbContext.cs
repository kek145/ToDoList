using ToDoList.Domain.DbSet;
using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure.Configurations;

namespace ToDoList.Infrastructure.DataStore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Note> Notes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NoteConfiguration());
    }
}