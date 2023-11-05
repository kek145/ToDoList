namespace ToDoList.BL.Mediator.Commands.TaskCommand;

public class DeleteTaskCommand : IRequest<bool>
{
    public int TaskId { get; private set; }

    public DeleteTaskCommand(int taskId)
    {
        TaskId = taskId;
    }
}