using MediatR;
using System.Net;
using FluentValidation;
using ToDoList.Domain.Result;
using System.Threading.Tasks;
using ToDoList.Domain.Request;
using ToDoList.Domain.Abstractions;
using ToDoList.Domain.Implementations;
using ToDoList.Application.Commands.Users.Create;

namespace ToDoList.Application.Services.RegistrationService;

public class RegistrationService(IMediator mediator, IValidator<RegistrationRequest> validator) : IRegistrationService
{
    private readonly IMediator _mediator = mediator;
    private readonly IValidator<RegistrationRequest> _validator = validator;

    // public RegistrationService(IMediator mediator, IValidator<RegistrationRequest> validator)
    // {
    //     _mediator = mediator;
    //     _validator = validator;
    // }


    public async Task<IBaseResponse<UserResponse>> RegistrationAsync(RegistrationRequest request)
    {
        var validation = await _validator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            return new BaseResponse<UserResponse>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation error",
                Errors = [$"{validation}"]
            };
        }
        // throw new BadRequestException($"Validation error: {validation}!");

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