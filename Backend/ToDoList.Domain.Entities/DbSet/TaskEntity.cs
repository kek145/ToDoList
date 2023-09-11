using System;
using ToDoList.Domain.Entities.Enums;

namespace ToDoList.Domain.Entities.DbSet;

public class TaskEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Status { get; set; }
    public Priority Priority { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}