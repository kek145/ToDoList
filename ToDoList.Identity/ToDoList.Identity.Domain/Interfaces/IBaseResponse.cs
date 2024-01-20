using System.Collections.Generic;
using System.Net;

namespace ToDoList.Identity.Domain.Interfaces;

public interface IBaseResponse<out T>
{
    string Status { get; }
    string Message { get; }
    HttpStatusCode StatusCode { get; }
    T Data { get; }
    List<string> Errors { get; set; }
}