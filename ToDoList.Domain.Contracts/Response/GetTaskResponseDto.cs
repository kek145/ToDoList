using System;

namespace ToDoList.Domain.Contracts.Response;

public class GetTaskResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Status { get; set; }
    public string Priority { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
}