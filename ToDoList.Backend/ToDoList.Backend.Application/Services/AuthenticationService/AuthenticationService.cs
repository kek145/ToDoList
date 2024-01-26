using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Request;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AuthenticationService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public Task<IBaseResponse<AuthenticationResponse>> LoginAsync(LoginRequest request)
    {
        throw new System.NotImplementedException();
    }
}