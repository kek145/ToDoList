namespace ToDoList.BL.Mediator.Queries.TaskQueries;

public class SearchTaskQuery : IRequest<PaginationResponse<GetTaskResponse>>
{
    public int Page { get; private set; }
    public int UserId { get; private set; }
    public string Search { get; private set; }

    public SearchTaskQuery(int page, int userId, string search)
    {
        Page = page;
        UserId = userId;
        Search = search;
    }
}