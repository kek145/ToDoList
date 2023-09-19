namespace ToDoList.BL.Mediator.Commands.TaskCommand;

public class CreateTaskCommand : IRequest<GetTaskResponse>
{
    public TaskRequest Request { get; private set; }
    public int UserId { get; private set; }

    public CreateTaskCommand(TaskRequest request, int userId)
    {
        Request = request;
        UserId = userId;
    }
}