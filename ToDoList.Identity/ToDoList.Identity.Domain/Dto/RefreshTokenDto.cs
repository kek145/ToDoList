using System;
using ToDoList.Identity.Domain.DbSet;

namespace ToDoList.Identity.Domain.Dto;

public class RefreshTokenDto
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public Guid JwtId { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime ExpiresToken { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = new();
}