using System;
using ToDoList.Domain.DbSet;

namespace ToDoList.Domain.Dto;

public class NoteDto
{
    public long Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public bool Status { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public int UserId { get; set; }
    public User User { get; init; } = new();
}