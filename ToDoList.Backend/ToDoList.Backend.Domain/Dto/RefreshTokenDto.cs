using System;
using ToDoList.Domain.DbSet;

namespace ToDoList.Domain.Dto;

public class RefreshTokenDto
{
    public int Id { get; init; }
    public string Token { get; init; } = string.Empty;
    public DateTime ExpiresDate { get; init; }
    public int UserId { get; init; }
    public User? User { get; init; }
}