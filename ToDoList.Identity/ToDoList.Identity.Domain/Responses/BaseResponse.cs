using System.Collections.Generic;
using System.Net;
using ToDoList.Identity.Domain.Interfaces;

namespace ToDoList.Identity.Domain.Responses;

public class BaseResponse<T> : IBaseResponse<T>
{

    public string Status { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public HttpStatusCode StatusCode { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; } = [];
}