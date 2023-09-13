using MediatR;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Mediator.Queries.TaskQueries;

public class GetTaskQuery : IRequest<GetTaskResponse>
{
    public int TaskId { get; private set; }

    public GetTaskQuery(int taskId)
    {
        TaskId = taskId;
    }
}