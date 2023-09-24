namespace ToDoList.BL.Mediator.Commands.TokenCommand;

public class SaveTokenHandler : IRequestHandler<SaveTokenCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public SaveTokenHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(SaveTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _unitOfWork.TokenRepository
            .GetAll()
            .Where(find => find.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

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
                ExpiresDate = DateTime.UtcNow.AddMonths(3),
                UserId = request.UserId
            };
            await _unitOfWork.TokenRepository.AddAsync(refresh);
            await _unitOfWork.CommitAsync();
        }
    }
}