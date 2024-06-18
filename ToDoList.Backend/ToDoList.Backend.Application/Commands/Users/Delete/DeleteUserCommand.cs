using MediatR;

namespace ToDoList.Application.Commands.Users.Delete;

public record DeleteUserCommand(int UserId) : IRequest<int>;