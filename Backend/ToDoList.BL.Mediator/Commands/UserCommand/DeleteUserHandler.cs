namespace ToDoList.BL.Mediator.Commands.UserCommand;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository
            .GetAll()
            .Where(x => x.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            return false;

        await _unitOfWork.UserRepository.DeleteAsync(user);
        await _unitOfWork.CommitAsync();

        return true;
    }
}