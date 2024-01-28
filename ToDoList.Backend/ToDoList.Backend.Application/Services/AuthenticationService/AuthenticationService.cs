using System;
using MediatR;
using AutoMapper;
using FluentValidation;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Request;
using Microsoft.AspNetCore.Http;
using ToDoList.Application.Exceptions;
using ToDoList.Infrastructure.Identity;
using ToDoList.Application.Queries.Users;
using ToDoList.Application.Services.TokenService;

namespace ToDoList.Application.Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;
    private readonly IValidator<LoginRequest> _validator;
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthenticationService(
        IMapper mapper,
        IMediator mediator,
        ITokenService tokenService,
        IValidator<LoginRequest> validator,
        IHttpContextAccessor contextAccessor)
    {
        _mapper = mapper;
        _mediator = mediator;
        _tokenService = tokenService;
        _validator = validator;
        _contextAccessor = contextAccessor;
    }

    public async Task<AuthenticationResponse> LoginAsync(LoginRequest request)
    {
        var validation = await _validator.ValidateAsync(request);

        if (!validation.IsValid)
            throw new BadRequestException($"Validation error: {validation}");

        var query = new GetUserByEmailQuery(request.Email);

        var user = await _mediator.Send(query);

        if (!PasswordHasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            throw new UnauthorizedAccessException("Invalid password");

        var response = _mapper.Map<UserResponse>(user);

        var tokens = _tokenService.GenerateTokens(response);

        await _tokenService.SaveTokenAsync(response.UserId, tokens.RefreshToken);
        
        _contextAccessor.HttpContext.Response.Cookies.Append("X-Refresh-Token", tokens.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
        
        _contextAccessor.HttpContext.Response.Cookies.Append("X-Access-Token", tokens.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });

        return new AuthenticationResponse
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken
        };
    }
}