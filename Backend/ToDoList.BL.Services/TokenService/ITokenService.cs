using ToDoList.Domain.Entities.DbSet;

namespace ToDoList.BL.Services.TokenService;

public interface ITokenService
{
    Task DeleteTokenAsync(string refreshToken);
    Task SaveTokenAsync(int userId, string refreshToken);
    Task<bool> ValidationRefreshTokenAsync(string refreshToken);
    AuthenticationResponse GenerateTokens(GetUserResponse entity);
    Task<AuthenticationResponse> RefreshTokenAsync(string refreshToken);
}