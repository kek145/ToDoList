using ToDoList.DAL.Core;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using ToDoList.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ToDoList.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();