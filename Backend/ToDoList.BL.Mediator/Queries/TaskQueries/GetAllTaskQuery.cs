using MediatR;
using System.Collections.Generic;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Mediator.Queries.TaskQueries;

public class GetAllTaskQuery : IRequest<TaskResponse<GetTaskResponse>>
{
    public int Page { get; private set; }

    public GetAllTaskQuery(int page)
    {
        Page = page;
    }
}