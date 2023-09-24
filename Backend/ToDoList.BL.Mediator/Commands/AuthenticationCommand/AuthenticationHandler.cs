using ToDoList.Security.HashData;

namespace ToDoList.BL.Mediator.Commands.AuthenticationCommand;

public class AuthenticationHandler : IRequestHandler<AuthenticationCommand, GetUserResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AuthenticationHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUserResponse> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository
            .GetAll()
            .Where(find => find.Email == request.Request.Email)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user == null || !PasswordHasher.VerifyPasswordHash(request.Request.Password, user.PasswordHash, user.PasswordSalt))
            throw new UnauthorizedException("Invalid email or password");
        
        var result = _mapper.Map<GetUserResponse>(user);

        return result;
    }
}