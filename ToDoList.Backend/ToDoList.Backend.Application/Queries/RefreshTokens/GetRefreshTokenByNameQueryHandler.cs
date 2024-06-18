using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Exceptions;

namespace ToDoList.Application.Queries.RefreshTokens;

public class GetRefreshTokenByNameQueryHandler(IRefreshTokenRepository refreshTokenRepository) : IRequestHandler<GetRefreshTokenByNameQuery, RefreshTokenDto>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    
    public async Task<RefreshTokenDto> Handle(GetRefreshTokenByNameQuery request, CancellationToken cancellationToken)
    {
        var token = await _refreshTokenRepository
            .GetAll()
            .FirstOrDefaultAsync(x => x.Token == request.RefreshToken, cancellationToken);

        if (token is null)
            throw new NotFoundException("Маркер оновлення не знайдено!");
        
        return token;
    }
}