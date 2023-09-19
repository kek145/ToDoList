namespace ToDoList.BL.Mediator.Commands.TaskCommand;

public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository
            .GetAll()
            .Where(x => x.Id == request.TaskId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (task == null)
            return false;

        task.Title = request.Request.Title;
        task.Description = request.Request.Description;
        task.Priority = request.Request.Priority;
        task.Deadline = request.Request.DeadLine;
        task.UpdatedAt = DateTime.UtcNow;
        task.UserId = task.UserId;

        await _unitOfWork.TaskRepository.UpdateAsync(task);
        await _unitOfWork.CommitAsync();

        return true;
    }
}