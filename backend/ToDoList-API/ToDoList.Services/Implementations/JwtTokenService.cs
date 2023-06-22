using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using ToDoList.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Configurations.Configurations;

namespace ToDoList.Services.Implementations
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly ILogger<JwtTokenService> _logger;
        private readonly JwtConfiguration _jwtConfiguration;

        public JwtTokenService(IOptions<JwtConfiguration> jwtConfiguration, ILogger<JwtTokenService> logger)
        {
            _jwtConfiguration = jwtConfiguration.Value;
            _logger = logger;
        }
        public int? GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtConfiguration.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtConfiguration.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey)),
                ValidateIssuerSigningKey = true
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
        
                var userIdClaim = claimsPrincipal.FindFirst("userid");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
                {
                    return userId;
                }
                throw new InvalidOperationException("Invalid user ID claim");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Token validation failed");
            }

            return null;
        }

    }
}