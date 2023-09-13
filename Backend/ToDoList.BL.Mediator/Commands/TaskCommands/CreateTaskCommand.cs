using MediatR;
using ToDoList.Domain.Contracts.Request;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Mediator.Commands.TaskCommands;

public class CreateTaskCommand : IRequest<GetTaskResponse>
{
    public TaskRequest Request { get; private set; }

    public CreateTaskCommand(TaskRequest request)
    {
        Request = request;
    }
}