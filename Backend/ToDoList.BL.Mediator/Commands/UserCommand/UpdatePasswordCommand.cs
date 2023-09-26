namespace ToDoList.BL.Mediator.Commands.UserCommand;

public class UpdatePasswordCommand : IRequest<bool>
{
    public int UserId { get; private set; }
    public ChangePasswordRequest Request { get; private set; }

    public UpdatePasswordCommand(int userId, ChangePasswordRequest request)
    {
        UserId = userId;
        Request = request;
    }
}