using System;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities.DbSet;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.DAL.Configurations.Configurations;

public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired();
        
        builder.Property(x => x.Status)
            .HasDefaultValue(false);
        
        builder.Property(x => x.Priority)
            .HasMaxLength(1)
            .IsRequired();
        
        builder.Property(x => x.Deadline)
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .HasDefaultValue(DateTime.UtcNow);
        
        builder.Property(x => x.UpdatedAt)
            .HasDefaultValue(DateTime.UtcNow);
    }
}