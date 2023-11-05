using MediatR;

namespace ToDoList.BL.Commands.TaskCommands;

public class DeleteTaskCommand : IRequest
{
    public int TaskId { get; set; }

    public DeleteTaskCommand(int taskId)
    {
        TaskId = taskId;
    }
}