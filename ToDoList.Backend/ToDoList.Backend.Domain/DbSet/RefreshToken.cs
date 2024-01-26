using System;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Domain.DbSet;

public class RefreshToken : IEntityId<int>
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresDate { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = new();
}