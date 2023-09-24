namespace ToDoList.BL.Mediator.Commands.UserCommand;

public class DeleteUserCommand : IRequest<bool>
{
    public int UserId { get; private set; }

    public DeleteUserCommand(int userId)
    {
        UserId = userId;
    }
}