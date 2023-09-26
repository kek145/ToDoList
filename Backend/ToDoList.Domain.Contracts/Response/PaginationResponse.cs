using System.Collections.Generic;

namespace ToDoList.Domain.Contracts.Response;

public class PaginationResponse<T>
{
    public List<T> Items { get; set; } = new();
    public int CurrentPage { get; set; }
    public int Pages { get; set; }
}