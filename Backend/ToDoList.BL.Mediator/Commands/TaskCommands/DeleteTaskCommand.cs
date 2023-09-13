using MediatR;

namespace ToDoList.BL.Mediator.Commands.TaskCommands;

public class DeleteTaskCommand : IRequest<bool>
{
    public int TaskId { get; private set; }

    public DeleteTaskCommand(int taskId)
    {
        TaskId = taskId;
    }
}