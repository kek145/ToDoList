using System;
using MediatR;
using AutoMapper;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ToDoList.Application.Exceptions;
using Microsoft.Extensions.Configuration;
using ToDoList.Application.Queries.RefreshTokens;
using ToDoList.Application.Queries.Users.GetById;
using ToDoList.Application.Commands.RefreshTokens.Create;
using ToDoList.Application.Commands.RefreshTokens.Delete;

namespace ToDoList.Application.Services.TokenService;

public class TokenService(IMapper mapper, IMediator mediator, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : ITokenService
{
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private readonly IConfiguration _configuration = configuration;
    private readonly IHttpContextAccessor _contextAccessor = httpContextAccessor;

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

    public async Task<AuthenticationResponse> RefreshTokenAsync()
    {
        var refreshToken = _contextAccessor.HttpContext.Request.Cookies["X-Refresh-Token"];

        if (refreshToken is null)
            throw new NotFoundException("Refresh token not found!");
        
        var query = new GetRefreshTokenByNameQuery(refreshToken);

        var token = await _mediator.Send(query);

        if (token.ExpiresDate < DateTime.UtcNow)
        {
            var command = new DeleteTokenCommand(token.UserId);

            var result = await _mediator.Send(command);

            if (result <= 0)
                throw new NotFoundException("User not found!");

            throw new UnauthorizedAccessException("Refresh token expires!");
        }

        var user = new GetUserByIdQuery(token.UserId);

        var userData = await _mediator.Send(user);

        var payload = _mapper.Map<UserResponse>(userData);

        var tokens = GenerateTokens(payload);

        await SaveTokenAsync(userData.Id, tokens.RefreshToken);
        
        _contextAccessor.HttpContext.Response.Cookies.Append("X-Refresh-Token", tokens.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(30)
        });
        
        _contextAccessor.HttpContext.Response.Cookies.Append("X-Access-Token", tokens.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddHours(1)
        });

        return new AuthenticationResponse
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken
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
                new Claim(JwtRegisteredClaimNames.Sub, response.FullName),
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
    
    private static string GenerateRefreshToken(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}