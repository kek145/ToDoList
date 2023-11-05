using MediatR;
using ToDoList.Domain.Contracts.Request;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Commands.TaskCommands;

public class CreateTaskCommand : IRequest<GetTaskResponseDto>
{
    public TaskRequestDto Request { get; private set; }

    public CreateTaskCommand(TaskRequestDto request)
    {
        Request = request;
    }
}