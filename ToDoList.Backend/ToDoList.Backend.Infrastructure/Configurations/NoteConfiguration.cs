using System;
using ToDoList.Domain.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Infrastructure.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(2000);
        
        // builder.Property(x => x.Priority)
        //     .IsRequired()
        //     .HasMaxLength(6)
        //     .HasConversion<string>();

        builder.Property(x => x.Status)
            .HasDefaultValue(false);
        
        builder.Property(x => x.Deadline)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasDefaultValue(DateTime.UtcNow);
        
        builder.Property(x => x.UserId)
            .IsRequired();
    }
}