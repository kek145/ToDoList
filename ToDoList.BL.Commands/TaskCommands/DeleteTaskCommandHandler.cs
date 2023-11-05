using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.DAL.Repositories.UnitOfWork;

namespace ToDoList.BL.Commands.TaskCommands;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);

        await _unitOfWork.TaskRepository.DeleteAsync(task);
        await _unitOfWork.CommitAsync();
    }
}