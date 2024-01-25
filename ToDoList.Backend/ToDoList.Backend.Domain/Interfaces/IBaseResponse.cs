using System.Collections.Generic;
using System.Net;

namespace ToDoList.Domain.Interfaces;

public interface IBaseResponse<T>
{
    HttpStatusCode StatusCode { get; set; }
    string Message { get; set; }
    T Data { get; set; }
    List<string> Errors { get; set; }
}