using MediatR;
using AutoMapper;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Exceptions;
using ToDoList.Infrastructure.Identity;

namespace ToDoList.Application.Commands.Users.Create;

public class CreateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, UserResponse>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    

    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userDto = _mapper.Map<UserDto>(request.RegistrationRequest);
        
        PasswordHasher.CreatePasswordHash(request.RegistrationRequest.Password, out var passwordHash, out var passwordSalt);
        
        var isEmailExists = await _unitOfWork.Users
            .GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == request.RegistrationRequest.Email, cancellationToken);

        if (isEmailExists is not null)
            throw new BadRequestException($"User with this email: {request.RegistrationRequest.Email} is already registered!");

        userDto.PasswordHash = passwordHash;
        userDto.PasswordSalt = passwordSalt;

        var addUser = await _unitOfWork.Users.AddUserAsync(userDto, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        var savedUser = await _unitOfWork.Users
            .GetAll()
            .FirstOrDefaultAsync(x => x.Email == addUser.Email, cancellationToken);
        
        return _mapper.Map<UserResponse>(savedUser);
    }
}