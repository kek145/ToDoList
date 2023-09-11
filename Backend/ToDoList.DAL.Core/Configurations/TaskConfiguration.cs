using System;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities.DbSet;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.DAL.Core.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.Property(task => task.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(task => task.Description)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(task => task.Status)
            .HasDefaultValue(false);
        builder.Property(task => task.Priority)
            .IsRequired();
        builder.Property(task => task.Deadline)
            .IsRequired();
        builder.Property(task => task.CreatedAt)
            .HasDefaultValue(DateTime.UtcNow);
        builder.Property(task => task.UpdatedAt)
            .HasDefaultValue(DateTime.UtcNow);
    }
}