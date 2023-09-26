namespace ToDoList.BL.Mediator.Commands.TokenCommand;

public class RefreshTokenCommand : IRequest<GetUserResponse>
{
    public string RefreshToken { get; private set; }

    public RefreshTokenCommand(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}