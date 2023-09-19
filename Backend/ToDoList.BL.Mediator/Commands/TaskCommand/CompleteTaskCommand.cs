namespace ToDoList.BL.Mediator.Commands.TaskCommand;

public class CompleteTaskCommand : IRequest<bool>
{
    public int TaskId { get; private set; }

    public CompleteTaskCommand(int taskId)
    {
        TaskId = taskId;
    }
}