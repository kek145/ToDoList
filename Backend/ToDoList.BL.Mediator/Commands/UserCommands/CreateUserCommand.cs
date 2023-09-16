using MediatR;
using ToDoList.Domain.Contracts.Request;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Mediator.Commands.UserCommands;

public class CreateUserCommand : IRequest<GetUserResponse>
{
    public RegistrationRequest Request { get; private set; }

    public CreateUserCommand(RegistrationRequest request)
    {
        Request = request;
    }
}