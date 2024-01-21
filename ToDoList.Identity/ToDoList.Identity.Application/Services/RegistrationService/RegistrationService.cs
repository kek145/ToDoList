using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using System.Threading.Tasks;
using ToDoList.Identity.Domain.Requests;
using ToDoList.Identity.Domain.Responses;
using ToDoList.Identity.Domain.Interfaces;
using ToDoList.Identity.Application.Helpers;
using ToDoList.Identity.Application.Exceptions;
using ToDoList.Identity.Application.Commands.UserCommands;

namespace ToDoList.Identity.Application.Services.RegistrationService;

public class RegistrationService : IRegistrationService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IValidator<RegistrationRequest> _validator;

    public RegistrationService(IMapper mapper, IMediator mediator, IValidator<RegistrationRequest> validator)
    {
        _mapper = mapper;
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<IBaseResponse<UserResponse>> RegistrationUserAsync(RegistrationRequest request)
    {
        var validation = await _validator.ValidateAsync(request);

        if (!validation.IsValid)
            throw new BadRequestException($"{validation}");

        var command = new CreateUserCommand(request);

        var user = await _mediator.Send(command);

        var data = _mapper.Map<UserResponse>(user);

        return new BaseResponse<UserResponse>
        {
            Status = ResponseHelper.Success,
            Message = ResponseHelper.RegistrationSuccess,
            StatusCode = HttpStatusCode.Created,
            Data = data
        };
    }
}