using MediatR;

namespace ToDoList.Application.Commands.RefreshTokens.Create;

public sealed record CreateTokenCommand(int UserId, string RefreshToken) : IRequest;