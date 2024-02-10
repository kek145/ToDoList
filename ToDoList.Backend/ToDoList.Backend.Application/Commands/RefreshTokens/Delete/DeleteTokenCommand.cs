using MediatR;

namespace ToDoList.Application.Commands.RefreshTokens.Delete;

public record DeleteTokenCommand(int UserId) : IRequest<int>;