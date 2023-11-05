namespace ToDoList.BL.Mediator.Commands.AuthenticationCommand;

public class AuthenticationCommand : IRequest<GetUserResponse>
{
    public AuthenticationRequest Request { get; private set; }

    public AuthenticationCommand(AuthenticationRequest request)
    {
        Request = request;
    }
}