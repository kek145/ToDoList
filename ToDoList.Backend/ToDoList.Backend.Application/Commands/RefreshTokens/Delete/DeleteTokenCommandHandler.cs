using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Commands.RefreshTokens.Delete;

public class DeleteTokenCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteTokenCommand, int>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<int> Handle(DeleteTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _unitOfWork.RefreshTokens
            .DeleteRefreshTokenAsync(request.UserId, cancellationToken);

        return token;
    }
}