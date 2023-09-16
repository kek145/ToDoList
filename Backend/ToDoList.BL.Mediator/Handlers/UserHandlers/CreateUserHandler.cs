using System.Linq;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Contracts.Response;
using ToDoList.BL.Mediator.Commands.UserCommands;
using ToDoList.DAL.Contracts.Interfaces;
using ToDoList.Domain.Entities.DbSet;
using ToDoList.Security.HashData;

namespace ToDoList.BL.Mediator.Handlers.UserHandlers;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, GetUserResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository
            .GetAll()
            .Where(find => find.Email == request.Request.Email)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (user != null)
            return null!;

        var mapper = _mapper.Map<UserEntity>(request.Request);

        PasswordHasher.CreatePasswordHash(request.Request.Password, out var passwordHash, out var passwordSalt);
        
        mapper.PasswordHash = passwordHash;
        mapper.PasswordSalt = passwordSalt;

        await _unitOfWork.UserRepository.CreateAsync(mapper);
        await _unitOfWork.CommitAsync();

        var result = _mapper.Map<GetUserResponse>(mapper);

        return result;
    }
}