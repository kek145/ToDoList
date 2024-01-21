    using System.Text;
    using ToDoList.Application;
    using System.Security.Claims;
    using ToDoList.Infrastructure;
    using ToDoList.Api.Extensions;
    using Microsoft.OpenApi.Models;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Swashbuckle.AspNetCore.Filters;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddApplication().AddInfrastructure(builder.Configuration);

    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme(\"Bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

    var issuer = builder.Configuration.GetSection("JWTConfiguration:Issuer").Value!; 
    var audience = builder.Configuration.GetSection("JWTConfiguration:Audience").Value!;
    var authority = builder.Configuration.GetSection("JWTConfiguration:Authority").Value!;
    var key = builder.Configuration.GetSection("JWTConfiguration:JWT_ACCESS_SECRET").Value!;

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.Authority = authority;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            RequireAudience = true,
            RequireExpirationTime = true,
            RoleClaimType = ClaimTypes.Role,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        };
    });

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