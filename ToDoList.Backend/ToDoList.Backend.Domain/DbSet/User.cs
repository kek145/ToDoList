using System.Collections.Generic;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Domain.DbSet;

public class User : IEntityId<int>
{
    public int Id { get; set; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public byte[] PasswordHash { get; init; } = [];
    public byte[] PasswordSalt { get; init; } = [];
    public List<Note> Notes { get; init; } = null!;
    public List<RefreshToken> RefreshToken { get; init; } = [];
}