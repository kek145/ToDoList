﻿using System;
using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Exceptions;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Commands.RefreshTokens.Create;

public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTokenCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

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
                ExpiresDate = DateTime.UtcNow.AddDays(30),
                UserId = request.UserId
            };
            await _unitOfWork.RefreshTokens.AddRefreshTokenAsync(newToken, cancellationToken);
        }
    }
}