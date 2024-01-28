using MediatR;

namespace ToDoList.Application.Commands.RefreshTokens;

public record CreateTokenCommand(int UserId, string RefreshToken) : IRequest;