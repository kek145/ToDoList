using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Identity.Domain.Responses;

namespace ToDoList.Identity.Application.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AuthenticationResponse GenerateTokens(UserResponse response)
    {
        var accessToken = GenerateAccessToken(response);

        return new AuthenticationResponse
        {
            AccessToken = accessToken,
            RefreshToken = ""
        };
    }
    
    private string GenerateAccessToken(UserResponse entity)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var secretKey = Encoding.UTF8.GetBytes(_configuration.GetSection("JWTConfiguration:JWT_ACCESS_SECRET").Value!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("UserId", entity.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, entity.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUniversalTime().ToString(CultureInfo.CurrentCulture))
            }),
            Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JWTConfiguration:ExpirationAccessToken").Value!)),
            Issuer = _configuration.GetSection("JWTConfiguration:Issuer").Value,
            Audience = _configuration.GetSection("JWTConfiguration:Audience").Value,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);

        return jwtTokenHandler.WriteToken(token);
    }
}