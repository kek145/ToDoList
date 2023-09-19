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
        var token = await _unitOfWork.TokenRepository.GetAll()
            .Where(find => find.RefreshToken == request.RefreshToken && find.ExpiresDate > DateTime.UtcNow)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        var removeToken = await _unitOfWork.TokenRepository.GetAll()
            .Where(find => find.RefreshToken == request.RefreshToken && find.ExpiresDate < DateTime.UtcNow)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (removeToken != null)
            await _unitOfWork.TokenRepository.DeleteAsync(removeToken);

        return token != null;
    }
}