using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Exceptions;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Queries.RefreshTokens;

public class GetRefreshTokenByNameQueryHandler : IRequestHandler<GetRefreshTokenByNameQuery, RefreshTokenDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRefreshTokenByNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<RefreshTokenDto> Handle(GetRefreshTokenByNameQuery request, CancellationToken cancellationToken)
    {
        var token = await _unitOfWork.RefreshTokens
            .GetAll()
            .FirstOrDefaultAsync(x => x.Token == request.RefreshToken, cancellationToken);

        if (token is null)
            throw new NotFoundException("Refresh token not found!");
        
        return token;
    }
}