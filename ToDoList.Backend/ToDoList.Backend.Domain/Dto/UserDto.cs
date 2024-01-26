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
    public List<NoteDto> Notes { get; set; } = [];
    public List<RefreshTokenDto> RefreshTokens { get; set; } = [];
}