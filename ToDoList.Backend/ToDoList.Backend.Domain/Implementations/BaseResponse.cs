using System.Net;
using System.Collections.Generic;
using ToDoList.Domain.Abstractions;

namespace ToDoList.Domain.Implementations;

public class BaseResponse<T> : IBaseResponse<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public T Data { get; set; }
    public List<string> Errors { get; set; } = [];
}