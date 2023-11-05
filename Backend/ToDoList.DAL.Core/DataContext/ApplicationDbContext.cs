namespace ToDoList.DAL.Core.DataContext;

public class ApplicationDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<TaskEntity> Tasks { get; set; } = null!;
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenEntityConfiguration());

        modelBuilder.Entity<TaskEntity>()
            .HasOne(user => user.User)
            .WithMany(task => task.Task)
            .HasForeignKey(key => key.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_task_user");

        modelBuilder.Entity<RefreshTokenEntity>()
            .HasOne(user => user.User)
            .WithMany(token => token.RefreshToken)
            .HasForeignKey(key => key.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_token_user");
    }
}