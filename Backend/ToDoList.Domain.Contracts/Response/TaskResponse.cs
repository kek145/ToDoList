using System.Collections.Generic;

namespace ToDoList.Domain.Contracts.Response;

public class TaskResponse<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int CurrentPage { get; set; }
    public int Pages { get; set; }
}