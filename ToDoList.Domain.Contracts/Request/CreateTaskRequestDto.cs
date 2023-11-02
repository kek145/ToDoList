using System;
using ToDoList.Domain.Entities.Enum;

namespace ToDoList.Domain.Contracts.Request;

public class CreateTaskRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; }
    public DateTime DeadLine { get; set; }
}