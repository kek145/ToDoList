using MediatR;
using ToDoList.Domain.Dto;

namespace ToDoList.Application.Queries.Users.GetById;

public record GetUserByIdQuery(int UserId) : IRequest<UserDto>;