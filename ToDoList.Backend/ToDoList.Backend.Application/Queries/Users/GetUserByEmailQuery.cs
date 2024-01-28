using MediatR;
using ToDoList.Domain.Dto;

namespace ToDoList.Application.Queries.Users;

public record GetUserByEmailQuery(string Email) : IRequest<UserDto>;