using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Contracts.Interfaces;
using ToDoList.BL.Mediator.Commands.TaskCommands;

namespace ToDoList.BL.Mediator.Handlers.TaskHandlers;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository
            .GetAll()
            .Where(find => find.Id == request.TaskId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (task == null)
            return false;

        await _unitOfWork.TaskRepository.RemoveAsync(task);
        await _unitOfWork.CommitAsync();

        return true;
    }
}