using System.Net;
using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using FluentValidation;
using ToDoList.Application.Commands.Users.Create;
using ToDoList.Application.Exceptions;
using ToDoList.Domain.Result;
using ToDoList.Domain.Request;
using ToDoList.Domain.Abstractions;
using ToDoList.Domain.Implementations;

namespace ToDoList.Application.Services.RegistrationService;

public class RegistrationService : IRegistrationService
{
    private readonly IMediator _mediator;
    private readonly IValidator<RegistrationRequest> _validator;

    public RegistrationService(IMediator mediator, IValidator<RegistrationRequest> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }


    public async Task<IBaseResponse<UserResponse>> RegistrationAsync(RegistrationRequest request)
    {
        var validation = await _validator.ValidateAsync(request);

        if (!validation.IsValid)
            throw new BadRequestException($"Validation error: {validation}!");

        var command = new CreateUserCommand(request);

        var data = await _mediator.Send(command);

        return new BaseResponse<UserResponse>
        {
            StatusCode = HttpStatusCode.Created,
            Message = "Registration completed successfully!",
            Data = data
        };
    }
}