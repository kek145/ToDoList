namespace ToDoList.BL.Mediator.Commands.TaskCommand;

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
            .FirstOrDefaultAsync(cancellationToken);

        if (task == null)
            return false;

        await _unitOfWork.TaskRepository.DeleteAsync(task);
        await _unitOfWork.CommitAsync();
        
        return true;
    }
}