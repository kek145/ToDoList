using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Request;

namespace ToDoList.Application.Services.RegistrationService;

public class RegistrationService : IRegistrationService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public RegistrationService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public Task<UserResponse> RegistrationAsync(RegistrationRequest request)
    {
        throw new System.NotImplementedException();
    }
}