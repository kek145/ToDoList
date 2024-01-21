using System;
using MediatR;
using FluentValidation;
using System.Threading.Tasks;
using AutoMapper;
using ToDoList.Identity.Domain.Requests;
using ToDoList.Identity.Domain.Responses;
using ToDoList.Identity.Application.Exceptions;
using ToDoList.Identity.Application.Queries.UserQueries;
using ToDoList.Identity.Application.Services.TokenService;
using ToDoList.Identity.Infrastructure.Identity;

namespace ToDoList.Identity.Application.Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;
    private readonly IValidator<LoginRequest> _validator;

    public AuthenticationService(IMapper mapper, IMediator mediator, ITokenService tokenService, IValidator<LoginRequest> validator)
    {
        _mapper = mapper;
        _mediator = mediator;
        _tokenService = tokenService;
        _validator = validator;
    }

    public async Task<AuthenticationResponse> LoginAsync(LoginRequest request)
    {
        var validation = await _validator.ValidateAsync(request);

        if (!validation.IsValid)
            throw new BadRequestException($"{validation}");

        var query = new GetUserByEmailQuery(request.Email);

        var user = await _mediator.Send(query);


        if (!PasswordHasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            throw new UnauthorizedAccessException("Invalid password!");

        var response = _mapper.Map<UserResponse>(user);

        var tokens = _tokenService.GenerateTokens(response);

        return new AuthenticationResponse
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = ""
        };
    }
}