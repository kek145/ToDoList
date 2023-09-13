using MediatR;
using System.Collections.Generic;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Mediator.Queries.TaskQueries;

public class GetAllTaskQuery : IRequest<IEnumerable<GetTaskResponse>> { }