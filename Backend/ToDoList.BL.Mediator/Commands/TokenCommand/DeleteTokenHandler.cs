namespace ToDoList.BL.Mediator.Commands.TokenCommand;

public class DeleteTokenHandler : IRequestHandler<DeleteTokenCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTokenHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(DeleteTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _unitOfWork.TokenRepository
            .GetAll()
            .Where(x => x.RefreshToken == request.RefreshToken)
            .FirstOrDefaultAsync(cancellationToken);

        if (token == null)
            return false;

        await _unitOfWork.TokenRepository.DeleteAsync(token);
        await _unitOfWork.CommitAsync();
        
        return true;
    }
}