namespace ToDoList.BL.Mediator.Commands.UserCommand;

public class UpdateEmailCommand : IRequest<bool>
{
    public int UserId { get; private set; }
    public ChangeEmailRequest Request { get; private set; }

    public UpdateEmailCommand(int userId, ChangeEmailRequest request)
    {
        UserId = userId;
        Request = request;
    }
}