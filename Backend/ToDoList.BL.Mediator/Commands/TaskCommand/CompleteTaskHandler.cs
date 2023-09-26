namespace ToDoList.BL.Mediator.Commands.TaskCommand;

public class CompleteTaskHandler : IRequestHandler<CompleteTaskCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public CompleteTaskHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository
            .GetAll()
            .Where(x => x.Id == request.TaskId)
            .FirstOrDefaultAsync(cancellationToken);

        if (task == null)
            return false;

        task.Status = true;

        await _unitOfWork.TaskRepository.UpdateAsync(task);
        await _unitOfWork.CommitAsync();

        return true;
    }
}