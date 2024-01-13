using System;
using System.Collections.Generic;
using ToDoList.Identity.Domain.Interfaces;

namespace ToDoList.Identity.Domain.DbSet;

public class User : IEntityId<int>
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public bool IsVerified { get; set; }
    public string VerificationToken { get; set; } = string.Empty;
    public DateTime VerifiedAt { get; set; }
    public string PasswordResetToken { get; set; } = string.Empty;
    public DateTime? ResetTokenExpires { get; set; }
    public List<RefreshToken> RefreshToken { get; set; } = [];
}