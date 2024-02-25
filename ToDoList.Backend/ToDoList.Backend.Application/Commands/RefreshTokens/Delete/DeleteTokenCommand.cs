using MediatR;

namespace ToDoList.Application.Commands.RefreshTokens.Delete;

public sealed record DeleteTokenCommand(int UserId) : IRequest<int>;