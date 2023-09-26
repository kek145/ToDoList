namespace ToDoList.BL.Mediator.Commands.UserCommand;

public class UpdateFullNameHandler : IRequestHandler<UpdateFullNameCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFullNameHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(UpdateFullNameCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository
            .GetAll()
            .Where(x => x.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            return false;

        user.FirstName = request.Request.FirstName;
        user.LastName = request.Request.LastName;

        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.CommitAsync();

        return true;
    }
}