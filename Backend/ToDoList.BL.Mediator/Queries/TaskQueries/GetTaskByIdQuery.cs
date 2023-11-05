namespace ToDoList.BL.Mediator.Queries.TaskQueries;

public class GetTaskByIdQuery : IRequest<GetTaskResponse>
{
    public int TaskId { get; private set; }

    public GetTaskByIdQuery(int taskId)
    {
        TaskId = taskId;
    }
}