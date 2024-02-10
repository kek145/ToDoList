using MediatR;

namespace ToDoList.Application.Commands.RefreshTokens.Create;

public record CreateTokenCommand(int UserId, string RefreshToken) : IRequest;