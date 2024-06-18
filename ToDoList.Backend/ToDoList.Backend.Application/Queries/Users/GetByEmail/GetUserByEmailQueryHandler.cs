using System;
using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Queries.Users.GetByEmail;

public class GetUserByEmailQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByEmailQuery, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var email = await _userRepository.GetUserByEmail(request.Email, cancellationToken);

        if (email is null)
            throw new UnauthorizedAccessException("Недійсна електронна адреса!");

        return email;
    }
}