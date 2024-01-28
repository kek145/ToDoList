using System;
using MediatR;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using ToDoList.Application.Commands.RefreshTokens.Create;
using ToDoList.Application.Commands.RefreshTokens.Delete;

namespace ToDoList.Application.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public TokenService(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    public async Task DeleteTokenAsync(int userId)
    {
        if (userId <= 0)
            throw new UnauthorizedAccessException("userId cannot be less than or equal to zero!");

        var command = new DeleteTokenCommand(userId);

        var result = await _mediator.Send(command);

        if (result <= 0)
            throw new UnauthorizedAccessException("userId cannot be less than or equal to zero!");
    }

    public async Task SaveTokenAsync(int userId, string refreshToken)
    {
        var command = new CreateTokenCommand(userId, refreshToken);
        await _mediator.Send(command);
    }

    public AuthenticationResponse GenerateTokens(UserResponse response)
    {
        var accessToken = GenerateAccessToken(response);
        var refreshToken = GenerateRefreshToken(255);

        return new AuthenticationResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };
    }
    
    private string GenerateAccessToken(UserResponse response)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var secretKey = Encoding.UTF8.GetBytes(_configuration.GetSection("JWTConfiguration:JWT_ACCESS_SECRET").Value!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("UserId", response.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, response.Email),
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
    
    private string GenerateRefreshToken(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}