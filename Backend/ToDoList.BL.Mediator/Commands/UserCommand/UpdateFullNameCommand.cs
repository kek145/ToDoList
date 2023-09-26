namespace ToDoList.BL.Mediator.Commands.UserCommand;

public class UpdateFullNameCommand : IRequest<bool>
{
    public int UserId { get; private set; }
    public ChangeFullNameRequest Request { get; private set; }

    public UpdateFullNameCommand(int userId, ChangeFullNameRequest request)
    {
        UserId = userId;
        Request = request;
    }
}