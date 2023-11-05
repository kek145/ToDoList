namespace ToDoList.BL.Mediator.Queries.TaskQueries;

public class GetAllCompletedTaskQuery : IRequest<PaginationResponse<GetTaskResponse>>
{
    public int Page { get; private set; }
    public int UserId { get; private set; }

    public GetAllCompletedTaskQuery(int page, int userId)
    {
        Page = page;
        UserId = userId;
    }
}