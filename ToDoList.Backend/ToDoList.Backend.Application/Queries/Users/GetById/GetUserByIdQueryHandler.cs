using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Repositories;
using ToDoList.Application.Exceptions;

namespace ToDoList.Application.Queries.Users.GetById;

public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetUserById(request.UserId, cancellationToken);

        if (user is null)
            throw new NotFoundException("Користувач не знайдений!");

        return user;
    }
}