using System.Collections.Generic;
using System.Net;
using ToDoList.Identity.Domain.Interfaces;

namespace ToDoList.Identity.Domain.Responses;

public class BaseResponse<T>(HttpStatusCode statusCode, string message, T data, string status, List<string> errors) : IBaseResponse<T>
{
    public string Status { get; } = status;
    public string Message { get; } = message;
    public HttpStatusCode StatusCode { get; } = statusCode;
    public T Data { get; } = data;
    public List<string> Errors { get; set; } = errors;
}