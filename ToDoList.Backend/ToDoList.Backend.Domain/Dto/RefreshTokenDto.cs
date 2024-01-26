using System;
using ToDoList.Domain.DbSet;

namespace ToDoList.Domain.Dto;

public class RefreshTokenDto
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresDate { get; set; }
    public int UserId { get; set; }
    public UserDto User { get; set; } = new();
}