using ToDoList.Identity.Domain.Interfaces;

namespace ToDoList.Identity.Domain.Dto;

public class UserDto : IEntityId<long>
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
}