using ToDoList.Security.HashData;

namespace ToDoList.BL.Mediator.Commands.RegistrationCommand;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, GetUserResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository
            .GetAll()
            .AsNoTracking()
            .Where(find => find.Email == request.Request.Email)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (user != null)
            throw new ConflictException($"A user with this email address {request.Request.Email} is already registered");

        PasswordHasher.CreatePasswordHash(request.Request.Password, out var passwordHash, out var passwordSalt);
        var mapper = _mapper.Map<UserEntity>(request.Request);

        mapper.PasswordHash = passwordHash;
        mapper.PasswordSalt = passwordSalt;

        await _unitOfWork.UserRepository.AddAsync(mapper);
        await _unitOfWork.CommitAsync();

        var result = _mapper.Map<GetUserResponse>(mapper);

        return result;
    }
}