namespace ToDoList.BL.Mediator.Commands.TokenCommand;

public class SaveTokenCommand : IRequest
{
    public int UserId { get; private set; }
    public string RefreshToken { get; private set; }

    public SaveTokenCommand(int userId, string refreshToken)
    {
        UserId = userId;
        RefreshToken = refreshToken;
    }
}