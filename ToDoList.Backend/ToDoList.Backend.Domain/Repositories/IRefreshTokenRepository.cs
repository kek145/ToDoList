using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Dto;

namespace ToDoList.Domain.Repositories;

public interface IRefreshTokenRepository
{
    IQueryable<RefreshTokenDto> GetAll();
    Task<int> DeleteRefreshTokenAsync(int tokenId, CancellationToken cancellationToken = default);
    Task<int> UpdateRefreshTokenAsync(int tokenId, string refreshToken, CancellationToken cancellationToken = default);
    Task<RefreshTokenDto> AddRefreshTokenAsync(RefreshTokenDto refreshTokenDto, CancellationToken cancellationToken = default);
}