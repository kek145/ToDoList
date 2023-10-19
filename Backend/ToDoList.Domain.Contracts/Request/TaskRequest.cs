using ToDoList.Domain.Entities.Enums;

namespace ToDoList.Domain.Contracts.Request;

public class TaskRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; }
    public DateTime DeadLine { get; set; }
}