using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.DbSet;

namespace ToDoList.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(1000);
        
        builder.Property(x => x.PasswordSalt)
            .IsRequired()
            .HasMaxLength(1000);
    }
}