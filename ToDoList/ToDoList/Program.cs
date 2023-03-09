using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ToDoList.Models;
using ToDoList.Models.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

new Thread(
    () =>
    {
        app.Run();
    }
).Start();

app.Run();
