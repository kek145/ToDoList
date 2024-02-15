using System;
using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Exceptions;

namespace ToDoList.Application.Commands.RefreshTokens.Create;

public class CreateTokenCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTokenCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _unitOfWork.RefreshTokens
            .GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

        if (token is not null)
        {
            var updateToken =
                await _unitOfWork.RefreshTokens.UpdateRefreshTokenAsync(token.Id, request.RefreshToken, cancellationToken);

            if (updateToken <= 0)
                throw new BadRequestException("token bad!");
        }
        
        else
        {
            var newToken = new RefreshTokenDto
            {
                Token = request.RefreshToken,
                ExpiresDate = DateTime.UtcNow.AddSeconds(30),
                UserId = request.UserId
            };
            await _unitOfWork.RefreshTokens.AddRefreshTokenAsync(newToken, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}