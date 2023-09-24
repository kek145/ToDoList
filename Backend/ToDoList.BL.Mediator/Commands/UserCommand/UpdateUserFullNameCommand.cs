namespace ToDoList.BL.Mediator.Commands.UserCommand;

public class UpdateUserFullNameCommand : IRequest<bool>
{
    public int UserId { get; private set; }
    public ChangeUserFullNameRequest Request { get; private set; }

    public UpdateUserFullNameCommand(int userId, ChangeUserFullNameRequest request)
    {
        UserId = userId;
        Request = request;
    }
}