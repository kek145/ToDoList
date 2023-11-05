namespace ToDoList.DAL.Core.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(user => user.FirstName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(user => user.LastName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(user => user.Email)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(user => user.PasswordHash)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(user => user.PasswordSalt)
            .HasMaxLength(500)
            .IsRequired();
    }
}