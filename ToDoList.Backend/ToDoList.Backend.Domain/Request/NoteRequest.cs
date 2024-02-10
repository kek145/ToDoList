using System;
using ToDoList.Domain.Enum;

namespace ToDoList.Domain.Request;

public class NoteRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; }
    public DateTime Deadline { get; set; }
}