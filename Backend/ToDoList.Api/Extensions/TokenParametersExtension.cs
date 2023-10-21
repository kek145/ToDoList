namespace ToDoList.Api.Extensions;

public static class TokenParametersExtension
{
    public static TokenValidationParameters Parameters(string issuer, string audience, byte[] secretKey)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };
    }
}