namespace ToDoList.BL.Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;
    private readonly IValidator<AuthenticationRequest> _validator;

    public AuthenticationService(
        IMediator mediator,
        ITokenService tokenService,
        IValidator<AuthenticationRequest> validator)
    {
        _mediator = mediator;
        _tokenService = tokenService;
        _validator = validator;
    }


    public async Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request)
    {
        var validator = await _validator.ValidateAsync(request);

        if (!validator.IsValid)
            throw new BadRequestException($"Validation error: {validator}");
        
        var result = await _mediator.Send(new AuthenticationCommand(request));

        var tokens = _tokenService.GenerateTokens(result);

        await _tokenService.SaveTokenAsync(result.UserId, tokens.RefreshToken);

        return tokens;
    }
}