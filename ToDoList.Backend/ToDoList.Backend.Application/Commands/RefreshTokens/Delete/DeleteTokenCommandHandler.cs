using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Commands.RefreshTokens.Delete;

public class DeleteTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository) : IRequestHandler<DeleteTokenCommand, int>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public async Task<int> Handle(DeleteTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _refreshTokenRepository.DeleteRefreshTokenAsync(request.UserId, cancellationToken);
        return token;
    }
}