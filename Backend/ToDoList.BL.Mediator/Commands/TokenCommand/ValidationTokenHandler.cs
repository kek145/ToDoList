namespace ToDoList.BL.Mediator.Commands.TokenCommand;

public class ValidationTokenHandler : IRequestHandler<ValidationTokenCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ValidationTokenHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ValidationTokenCommand request, CancellationToken cancellationToken)
    {
        var removeToken = await _unitOfWork.TokenRepository
            .GetAll()
            .Where(find => find.RefreshToken == request.RefreshToken && find.ExpiresDate < DateTime.UtcNow)
            .FirstOrDefaultAsync(cancellationToken);

        if (removeToken != null)
        {
            await _unitOfWork.TokenRepository.DeleteAsync(removeToken);
            await _unitOfWork.CommitAsync();
        }
        
        var token = await _unitOfWork.TokenRepository
            .GetAll()
            .Where(find => find.RefreshToken == request.RefreshToken && find.ExpiresDate > DateTime.UtcNow)
            .FirstOrDefaultAsync(cancellationToken);

        if (token == null)
            return false;

        return true;
    }
}