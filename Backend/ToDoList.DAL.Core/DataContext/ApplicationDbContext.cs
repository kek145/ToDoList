using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities.DbSet;
using ToDoList.DAL.Core.Configurations;

namespace ToDoList.DAL.Core.DataContext;

public class ApplicationDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<TaskEntity> Tasks { get; set; } = null!;
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TaskConfiguration());

        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.HasOne(t => t.User)
                .WithMany(u => u.Task)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_tasks_user");
        });

        modelBuilder.Entity<RefreshTokenEntity>(entity =>
        {
            entity.HasOne(t => t.User)
                .WithMany(u => u.RefreshToken)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_token_user");
        });
    }
}