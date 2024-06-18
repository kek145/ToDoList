using MediatR;
using AutoMapper;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Response;
using ToDoList.Domain.Repositories;
using ToDoList.Application.Exceptions;
using ToDoList.Infrastructure.Identity;

namespace ToDoList.Application.Commands.Users.Create;

public class CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository) : IRequestHandler<CreateUserCommand, UserResponse>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserRepository _userRepository = userRepository;
    

    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userDto = _mapper.Map<UserDto>(request.RegistrationRequest);
        
        PasswordHasher.CreatePasswordHash(request.RegistrationRequest.Password, out var passwordHash, out var passwordSalt);

        var isEmailExists = await _userRepository
            .GetUserByEmail(request.RegistrationRequest.Email, cancellationToken);

        if (isEmailExists is not null)
            throw new BadRequestException($"Користувач із цією електронною адресою: {request.RegistrationRequest.Email} вже зареєстрований!");

        userDto.PasswordHash = passwordHash;
        userDto.PasswordSalt = passwordSalt;

        var newUser = await _userRepository.AddUserAsync(userDto, cancellationToken);

        var result = _mapper.Map<UserResponse>(newUser);
        
        return result;
    }
}