using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Identity.Domain.DbSet;

namespace ToDoList.Identity.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Email)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(x => x.PasswordHash)
            .IsRequired();
        
        builder.Property(x => x.PasswordSalt)
            .IsRequired();
    }
}