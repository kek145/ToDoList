using System;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Domain.DbSet;

public class Note : IEntityId<long>, IBaseEntity
{
    public long Id { get; set; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Priority { get; init; } = string.Empty;
    public bool Status { get; init; }
    public DateTime Deadline { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int UserId { get; init; }
    public User User { get; init; } = new();
}