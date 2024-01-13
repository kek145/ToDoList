using System;
using ToDoList.Identity.Domain.Interfaces;

namespace ToDoList.Identity.Domain.DbSet;

public class RefreshToken : IEntityId<int>
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public Guid JwtId { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime ExpiresToken { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = new();
}