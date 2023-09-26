namespace ToDoList.BL.Mediator.Commands.UserCommand;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePasswordHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository
            .GetAll()
            .Where(x => x.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            return false;

        if (!PasswordHasher.VerifyPasswordHash(request.Request.CurrentPassword, user.PasswordHash, user.PasswordSalt))
            return false;
        
        PasswordHasher.CreatePasswordHash(request.Request.NewPassword, out var passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        
        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.CommitAsync();

        return true;
    }
}