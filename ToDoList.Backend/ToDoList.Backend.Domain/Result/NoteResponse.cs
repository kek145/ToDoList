using System;

namespace ToDoList.Domain.Result;

public class NoteResponse
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public bool Status { get; set; }
    public DateTime Deadline { get; set; }
}