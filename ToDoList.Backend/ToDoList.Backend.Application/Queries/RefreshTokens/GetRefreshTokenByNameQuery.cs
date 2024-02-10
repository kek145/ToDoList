using MediatR;
using ToDoList.Domain.Dto;

namespace ToDoList.Application.Queries.RefreshTokens;

public record GetRefreshTokenByNameQuery(string RefreshToken) : IRequest<RefreshTokenDto>;