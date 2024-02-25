using System.Collections.Generic;

namespace ToDoList.Domain.Response;

public class PagedResult<T>
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int RecordNumber { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; } = [];
}