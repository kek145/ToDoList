namespace ToDoList.BL.Mediator.Commands.TokenCommand;

public class DeleteTokenCommand : IRequest<bool>
{
    public string RefreshToken { get; private set; }

    public DeleteTokenCommand(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}