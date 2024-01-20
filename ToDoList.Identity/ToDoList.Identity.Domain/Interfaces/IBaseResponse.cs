using System.Net;

namespace ToDoList.Identity.Domain.Interfaces;

public interface IBaseResponse<out T>
{
    HttpStatusCode StatusCode { get; }
    string Message { get; }
    T Data { get; }
}