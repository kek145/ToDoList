using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Commands.Users.Delete;

public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, int>
{
    private readonly IUserRepository _userRepository = userRepository;
    
    public Task Handle(int request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.DeleteUserAsync(request.UserId, cancellationToken);
        return result;
    }
}