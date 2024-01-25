﻿using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ToDoList.Api.Extensions;

public static class TokenValidationParametersExtension
{
    public static TokenValidationParameters AddTokenParameters(IConfiguration configuration)
    {
        var issuer = configuration.GetSection("JWTConfiguration:Issuer").Value!; 
        var audience = configuration.GetSection("JWTConfiguration:Audience").Value!;
        var key = configuration.GetSection("JWTConfiguration:JWT_ACCESS_SECRET").Value!;
        
        return new TokenValidationParameters
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
    }
}