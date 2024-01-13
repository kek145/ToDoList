using ToDoList.Identity.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using ToDoList.Identity.Application;
using ToDoList.Identity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();