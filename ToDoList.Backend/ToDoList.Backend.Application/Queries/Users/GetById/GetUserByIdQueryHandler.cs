using MediatR;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Exceptions;

namespace ToDoList.Application.Queries.Users.GetById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users
            .GetAll()
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user is null)
            throw new NotFoundException("User not found!");

        return user;
    }
}