namespace ToDoList.BL.Mediator.Queries.UserQueries;

public class GetUserInfoQuery : IRequest<GetUserInfoResponse>
{
    public int UserId { get; private set; }

    public GetUserInfoQuery(int userId)
    {
        UserId = userId;
    }
}