namespace ToDoList.BL.Services.RegistrationService;

public class RegistrationService : IRegistrationService
{
    private readonly IMediator _mediator;
    private readonly IValidator<RegistrationRequest> _validator;

    public RegistrationService(
        IMediator mediator,
        IValidator<RegistrationRequest> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }
    public async Task<GetUserResponse> RegistrationAsync(RegistrationRequest request)
    {
        var validator = await _validator.ValidateAsync(request);

        if (!validator.IsValid)
            throw new BadRequestException($"{validator}");
        
        var result = await _mediator.Send(new CreateUserCommand(request));

        if (result == null)
            throw new BadRequestException("Result is null");
        
        return result;
    }
}