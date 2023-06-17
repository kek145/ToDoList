using System.Text;
using ToDoList.DAL.Core;
using ToDoList.DAL.Interfaces;
using Microsoft.OpenApi.Models;
using ToDoList.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using ToDoList.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using ToDoList.Services.Implementations;
using Microsoft.Extensions.Configuration;
using ToDoList.Configurations.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtConfiguration"));

var secretkey = builder.Configuration.GetSection("JwtConfiguration:SecretKey").Value;
var issuer = builder.Configuration.GetSection("JwtConfiguration:Issuer").Value;
var audience = builder.Configuration.GetSection("JwtConfiguration:Audience").Value;
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey!));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standart Authorization header using the Bearer scheme(\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true,
        IssuerSigningKey = signingKey,
        ValidateIssuerSigningKey = true
    };
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();