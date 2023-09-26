namespace ToDoList.BL.Mediator.Commands.RegistrationCommand;

public class CreateUserCommand : IRequest<GetUserResponse>
{
    public RegistrationRequest Request { get; private set; }

    public CreateUserCommand(RegistrationRequest request)
    {
        Request = request;
    }
}