using System;
using ToDoList.Domain.Abstractions;

namespace ToDoList.Domain.DbSet;

public class Note : IEntityId<long>, IBaseEntity
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public bool Status { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}