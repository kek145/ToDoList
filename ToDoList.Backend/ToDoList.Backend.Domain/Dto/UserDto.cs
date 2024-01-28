using System.Collections.Generic;
using ToDoList.Domain.DbSet;

namespace ToDoList.Domain.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public List<Note> Notes { get; set; } = null!;
    public List<RefreshToken>? RefreshToken { get; set; }
}