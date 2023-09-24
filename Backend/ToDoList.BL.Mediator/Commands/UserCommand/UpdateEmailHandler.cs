namespace ToDoList.BL.Mediator.Commands.UserCommand;

public class UpdateEmailHandler : IRequestHandler<UpdateEmailCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmailHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository
            .GetAll()
            .Where(x => x.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            return false;

        user.Email = request.Request.Email;

        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.CommitAsync();

        return true;
    }
}