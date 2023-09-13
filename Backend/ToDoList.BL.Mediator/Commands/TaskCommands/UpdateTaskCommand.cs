using MediatR;
using ToDoList.Domain.Contracts.Request;

namespace ToDoList.BL.Mediator.Commands.TaskCommands;

public class UpdateTaskCommand : IRequest<bool>
{
    public int TaskId { get; private set; }
    public TaskRequest Request { get; private set; }

    public UpdateTaskCommand(int taskId, TaskRequest request)
    {
        TaskId = taskId;
        Request = request;
    }
}