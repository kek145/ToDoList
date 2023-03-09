using ToDoList.WebApi;
using System.Threading;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

var todo = new ToDoMain();

new Thread(
    () =>
    {
        app.Run(todo.MainToDo);
    }
).Start();

app.Run();
