namespace ToDoList.BL.Mediator.Commands.TokenCommand;

public class SaveTokenHandler : IRequestHandler<SaveTokenCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public SaveTokenHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(SaveTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _unitOfWork.TokenRepository
            .GetAll()
            .AsNoTracking()
            .Where(find => find.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (token != null)
        {
            token.RefreshToken = request.RefreshToken;
            await _unitOfWork.TokenRepository.UpdateAsync(token);
            await _unitOfWork.CommitAsync();
        }
        else
        {
            var refresh = new RefreshTokenEntity
            {
                RefreshToken = request.RefreshToken,
                ExpiresDate = DateTime.UtcNow.AddDays(90),
                UserId = request.UserId
            };
            await _unitOfWork.TokenRepository.AddAsync(refresh);
        }
        await _unitOfWork.CommitAsync();
    }
}