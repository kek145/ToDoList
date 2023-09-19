namespace ToDoList.BL.Mediator.Queries.TaskQueries;

public class GetAllTaskByPriorityQuery : IRequest<PaginationResponse<GetTaskResponse>>
{
    public int Page { get; private set; }
    public int UserId { get; private set; }
    public string Priority { get; private set; }

    public GetAllTaskByPriorityQuery(int page, int userId, string priority)
    {
        Page = page;
        UserId = userId;
        Priority = priority;
    }
}