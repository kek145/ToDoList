using System.Net;
using ToDoList.Identity.Domain.Interfaces;

namespace ToDoList.Identity.Domain.Responses;

public class BaseResponse<T>(HttpStatusCode statusCode, string message, T data) : IBaseResponse<T>
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public string Message { get; } = message;
    public T Data { get; } = data;
}