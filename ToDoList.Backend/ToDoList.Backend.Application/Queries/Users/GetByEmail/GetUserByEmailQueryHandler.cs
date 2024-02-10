using System;
using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Queries.Users.GetByEmail;

public class GetUserByEmailQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserByEmailQuery, UserDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var email = await _unitOfWork.Users
            .GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (email is null)
            throw new UnauthorizedAccessException("Invalid email");

        return email;
    }
}