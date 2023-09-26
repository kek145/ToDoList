namespace ToDoList.BL.Mediator.Queries.TaskQueries;

public class GetAllFailedTaskQuery : IRequest<PaginationResponse<GetTaskResponse>>
{
    public int Page { get; private set; }
    public int UserId { get; private set; }

    public GetAllFailedTaskQuery(int page, int userId)
    {
        Page = page;
        UserId = userId;
    }
}