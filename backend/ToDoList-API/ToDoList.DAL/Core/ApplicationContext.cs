using Microsoft.EntityFrameworkCore;
using System;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;

namespace ToDoList.DAL.Core
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("users");
            modelBuilder.Entity<TaskEntity>().ToTable("tasks");

            modelBuilder.Entity<UserEntity>().HasKey(user => user.UserId);
            modelBuilder.Entity<TaskEntity>().HasKey(task => task.TaskId);

            modelBuilder.Entity<UserEntity>().HasIndex(user => user.Email).IsUnique();
            modelBuilder.Entity<TaskEntity>().HasIndex(task => task.Title).IsUnique();

            modelBuilder.Entity<UserEntity>(builder =>
            {
                builder.Property(user => user.UserId).HasColumnName("userid").IsRequired();
                builder.Property(user => user.UserName).HasColumnName("username").HasMaxLength(20).IsRequired();
                builder.Property(user => user.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
                builder.Property(user => user.Password).HasColumnName("password").HasMaxLength(255).IsRequired();
            });

            modelBuilder.Entity<TaskEntity>(builder =>
            {
                builder.Property(task => task.TaskId).HasColumnName("taskid").IsRequired();
                builder.Property(task => task.Title).HasColumnName("title").HasMaxLength(30).IsRequired();
                builder.Property(task => task.Description).HasColumnName("description").HasMaxLength(500).IsRequired();
                builder.Property(task => task.Status).HasColumnName("status").HasDefaultValue(false);
                builder.Property(task => task.Priority).HasColumnName("priority").HasDefaultValue(Priority.Easy);
                builder.Property(task => task.CreatedDate).HasColumnName("created").HasDefaultValue(DateTime.UtcNow);
                builder.Property(task => task.UserID).HasColumnName("user_id").IsRequired();
            });
        }
    }
}
