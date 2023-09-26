namespace ToDoList.BL.Mediator.Commands.TokenCommand;

public class ValidationTokenCommand : IRequest<bool>
{
    public string RefreshToken { get; private set; }

    public ValidationTokenCommand(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}