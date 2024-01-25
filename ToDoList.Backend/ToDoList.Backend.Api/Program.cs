using ToDoList.Application;
using ToDoList.Infrastructure;
using ToDoList.Api.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
    
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

builder.Services.AddApplication().AddInfrastructure(builder.Configuration);
    

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.AddGlobalErrorHandling();

app.MapControllers();

app.Run();