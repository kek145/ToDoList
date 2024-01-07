using ToDoList.Domain.DbSet;
using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure.Configurations;

namespace ToDoList.Infrastructure.DataStore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Note> Notes { get; set; } = null!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NoteConfiguration());
    }
}