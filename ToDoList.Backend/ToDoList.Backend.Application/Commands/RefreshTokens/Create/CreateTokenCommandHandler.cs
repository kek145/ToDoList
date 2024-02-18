using System;
using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Exceptions;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Commands.RefreshTokens.Create;

public class CreateTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository) : IRequestHandler<CreateTokenCommand>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public async Task Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _refreshTokenRepository
            .GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

        if (token is not null)
        {
            var updateToken =
                await _refreshTokenRepository.UpdateRefreshTokenAsync(token.Id, request.RefreshToken, cancellationToken);

            if (updateToken <= 0)
                throw new BadRequestException("token bad!");
        }
        
        else
        {
            var newToken = new RefreshTokenDto
            {
                Token = request.RefreshToken,
                ExpiresDate = DateTime.UtcNow.AddDays(30),
                UserId = request.UserId
            };
            await _refreshTokenRepository.AddRefreshTokenAsync(newToken, cancellationToken);
        }
    }
}