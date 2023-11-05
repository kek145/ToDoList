using System.Collections.Generic;

namespace ToDoList.BL.Mediator.Queries.TaskQueries;

public class GetAllTaskQuery : IRequest<PaginationResponse<GetTaskResponse>>
{
    public int Page { get; private set; }
    public int UserId { get; private set; }

    public GetAllTaskQuery(int page, int userId)
    {
        Page = page;
        UserId = userId;
    }
}