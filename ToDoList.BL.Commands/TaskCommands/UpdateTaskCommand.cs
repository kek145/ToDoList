using MediatR;
using ToDoList.Domain.Contracts.Request;

namespace ToDoList.BL.Commands.TaskCommands;

public class UpdateTaskCommand : IRequest
{
    public int TaskId { get; set; }
    public TaskRequestDto Request { get; private set; }

    public UpdateTaskCommand(int taskId, TaskRequestDto request)
    {
        TaskId = taskId;
        Request = request;
    }
}