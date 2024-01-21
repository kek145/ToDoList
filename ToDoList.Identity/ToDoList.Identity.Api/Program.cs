using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using ToDoList.Identity.Application;
using ToDoList.Identity.Infrastructure;
using ToDoList.Identity.Api.Extensions;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication().AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseGlobalErrorHandling();

app.Run();