using MediatR;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Queries.TaskQueries;

public class GetTaskByIdQuery : IRequest<GetTaskResponseDto>
{
    public int TaskId { get; private set; }

    public GetTaskByIdQuery(int taskId)
    {
        TaskId = taskId;
    }
}