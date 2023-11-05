namespace ToDoList.BL.Mediator.Queries.UserQueries;

public class GetUserFullNameQuery : IRequest<GetUserFullNameResponse>
{
    public int UserId { get; private set; }

    public GetUserFullNameQuery(int userId)
    {
        UserId = userId;
    }
}