using System.Collections.Generic;
using MediatR;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Queries.TaskQueries;

public class GetAllTaskQuery : IRequest<IEnumerable<GetTaskResponseDto>>
{
    
}