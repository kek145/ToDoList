using MediatR;

namespace ToDoList.Application.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly IMediator _mediator;

    public TokenService(IMediator mediator)
    {
        _mediator = mediator;
    }
}