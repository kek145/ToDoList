using ToDoList.Identity.Domain.Interfaces;

namespace ToDoList.Identity.Domain.DbSet;

public class User : IEntityId<long>
{
    public long Id { get; set; }
    public string Email { get; init; } = string.Empty;
    public byte[] PasswordHash { get; init; } = [];
    public byte[] PasswordSalt { get; init; } = [];
}