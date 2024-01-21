using MediatR;
using ToDoList.Identity.Domain.Dto;
using ToDoList.Identity.Domain.Requests;

namespace ToDoList.Identity.Application.Queries.UserQueries;

public record GetUserByEmailQuery(string Email) : IRequest<UserDto>;