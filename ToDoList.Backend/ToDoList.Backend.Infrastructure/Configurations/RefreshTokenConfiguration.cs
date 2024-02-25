﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.DbSet;

namespace ToDoList.Infrastructure.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.ExpiresDate)
            .IsRequired();

        builder.HasOne(u => u.User)
            .WithMany(r => r.RefreshToken)
            .HasForeignKey(k => k.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_token_user");

    }
}