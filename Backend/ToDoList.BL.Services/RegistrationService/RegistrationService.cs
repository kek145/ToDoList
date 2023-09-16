using System.Threading.Tasks;
using MediatR;
using ToDoList.BL.Mediator.Commands.UserCommands;
using ToDoList.Domain.Contracts.Request;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Services.RegistrationService;

public class RegistrationService : IRegistrationService
{
    private readonly IMediator _mediator;

    public RegistrationService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<GetUserResponse> RegistrationAsync(RegistrationRequest request)
    {
        var command = new CreateUserCommand(request);

        var result = await _mediator.Send(command);

        return result == null! ? null! : result;
    }
}