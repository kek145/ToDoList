using System.Text;
using System.Globalization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using ToDoList.BL.Mediator.Commands.TokenCommand;

namespace ToDoList.BL.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public TokenService(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    public async Task SaveTokenAsync(int userId, string refreshToken)
    {
        await _mediator.Send(new SaveTokenCommand(userId, refreshToken));
    }

    public async Task<bool> ValidationRefreshTokenAsync(string refreshToken)
    {
        var result = await _mediator.Send(new ValidationTokenCommand(refreshToken));

        if (!result)
            throw new UnauthorizedException("Your token has not been validated");
        
        return result;
    }

    public AuthenticationResponse GenerateTokens(GetUserResponse response)
    {
        var accessToken = GenerateAccessToken(response);
        var refreshToken = GenerateRefreshToken(254);

        return new AuthenticationResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<AuthenticationResponse> RefreshTokenAsync(string refreshToken)
    {
        var result = await _mediator.Send(new RefreshTokenCommand(refreshToken));

        if (result == null)
            throw new UnauthorizedException("Token not found!");

        var tokens = GenerateTokens(result);
        
        await SaveTokenAsync(result.UserId, tokens.RefreshToken);
        
        return new AuthenticationResponse
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken
        };
    }

    private string GenerateAccessToken(GetUserResponse entity)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var secretKey = Encoding.UTF8.GetBytes(_configuration.GetSection("JWTConfiguration:JWT_ACCESS_SECRET").Value!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("UserId", entity.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, entity.Email),
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
    
    private string GenerateRefreshToken(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}