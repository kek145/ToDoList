using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Identity.Domain.Dto;
using ToDoList.Identity.Domain.Interfaces;
using ToDoList.Identity.Application.Exceptions;
using ToDoList.Identity.Infrastructure.Identity;

namespace ToDoList.Identity.Application.Commands.UserCommands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == request.RegistrationRequest.Email, cancellationToken);
        
        if(user is not null)
            throw new BadRequestException($"User for this email: {request.RegistrationRequest.Email} is already exist!");
        
        var newUser = _mapper.Map<UserDto>(request.RegistrationRequest);

        PasswordHasher.CreatePasswordHash(request.RegistrationRequest.Password, out var passwordHash, out var passwordSalt);
        
        newUser.PasswordHash = passwordHash;
        newUser.PasswordSalt = passwordSalt;

        var result = await _unitOfWork.Users.AddUserAsync(newUser, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        
        return result;
    }
}