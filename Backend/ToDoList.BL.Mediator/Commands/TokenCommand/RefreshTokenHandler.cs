namespace ToDoList.BL.Mediator.Commands.TokenCommand;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, GetUserResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUserResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _unitOfWork.TokenRepository
            .GetAll()
            .Where(find => find.RefreshToken == request.RefreshToken && find.ExpiresDate > DateTime.UtcNow)
            .FirstOrDefaultAsync(cancellationToken);

        if (token == null)
            return null!;

        var user = await _unitOfWork.UserRepository
            .GetAll()
            .Where(find => find.Id == token.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            return null!;

        var result = _mapper.Map<GetUserResponse>(user);
        
        return result;
    }
}