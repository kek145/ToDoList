using System;
using ToDoList.Domain.Abstractions;

namespace ToDoList.Domain.DbSet;

public class RefreshToken : IEntityId<int>
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresDate { get; set; }
    public int UserId { get; set; }
    public virtual User? User { get; set; }
}