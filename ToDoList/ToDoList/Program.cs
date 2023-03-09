using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ToDoList;
using ToDoList.Models;
using ToDoList.Models.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

var todo = new ToDo();

new Thread(
    () =>
    {
        app.Run(todo.MainToDo);
    }
).Start();

app.Run();
