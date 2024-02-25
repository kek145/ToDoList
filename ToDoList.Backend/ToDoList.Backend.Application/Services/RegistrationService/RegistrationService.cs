using MediatR;
using System.Net;
using FluentValidation;
using System.Threading.Tasks;
using ToDoList.Domain.Request;
using ToDoList.Domain.Response;
using ToDoList.Domain.Abstractions;
using ToDoList.Domain.Implementations;
using ToDoList.Application.Exceptions;
using ToDoList.Application.Commands.Users.Create;

namespace ToDoList.Application.Services.RegistrationService;

public class RegistrationService(IMediator mediator, IValidator<RegistrationRequest> validator) : IRegistrationService
{
    private readonly IMediator _mediator = mediator;
    private readonly IValidator<RegistrationRequest> _validator = validator;


    public async Task<IBaseResponse<UserResponse>> RegistrationAsync(RegistrationRequest request)
    {
        var validation = await _validator.ValidateAsync(request);

        if (!validation.IsValid)
            throw new BadRequestException($"{validation}");

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