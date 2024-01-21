using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Identity.Domain.Dto;
using ToDoList.Identity.Domain.Interfaces;

namespace ToDoList.Identity.Application.Queries.UserQueries;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByEmailQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users
            .GetAll()
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user is null)
            throw new UnauthorizedAccessException("Invalid email!");

        return user;
    }
}