using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Core.DataContext;
using ToDoList.BL.Services.TaskService;
using ToDoList.DAL.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using ToDoList.Domain.Contracts.Response;
using ToDoList.DAL.Repositories.Repositories;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.BL.Mediator.Queries.TaskQueries;
using ToDoList.BL.Mediator.Commands.TaskCommands;
using ToDoList.BL.Mediator.Commands.UserCommands;
using ToDoList.BL.Mediator.Handlers.TaskHandlers;
using ToDoList.BL.Mediator.Handlers.UserHandlers;
using ToDoList.BL.Services.RegistrationService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IRequestHandler<UpdateTaskCommand, bool>, UpdateTaskHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteTaskCommand, bool>, DeleteTaskHandler>();
builder.Services.AddScoped<IRequestHandler<CreateTaskCommand, GetTaskResponse>, CreateTaskHandler>();
builder.Services.AddScoped<IRequestHandler<CreateUserCommand, GetUserResponse>, CreateUserHandler>();

builder.Services.AddTransient<IRequestHandler<GetTaskQuery, GetTaskResponse>, GetTaskHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllTaskQuery, TaskResponse<GetTaskResponse>>, GetAllTaskHandler>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(Program).Assembly));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseSnakeCaseNamingConvention();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();