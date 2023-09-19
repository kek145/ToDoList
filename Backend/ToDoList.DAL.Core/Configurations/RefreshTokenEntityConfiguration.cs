namespace ToDoList.DAL.Core.Configurations;

public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.Property(token => token.RefreshToken)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(token => token.ExpiresDate)
            .IsRequired();
    }
}