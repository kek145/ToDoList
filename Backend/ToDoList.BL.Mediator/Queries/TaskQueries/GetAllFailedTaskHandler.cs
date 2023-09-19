using System.Collections.Generic;

namespace ToDoList.BL.Mediator.Queries.TaskQueries;

public class GetAllFailedTaskHandler : IRequestHandler<GetAllFailedTaskQuery, PaginationResponse<GetTaskResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllFailedTaskHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<PaginationResponse<GetTaskResponse>> Handle(GetAllFailedTaskQuery request, CancellationToken cancellationToken)
    {
        const float pageResult = 10f;
        var pageCount = Math.Ceiling(_unitOfWork.TaskRepository.GetAll().Count() / pageResult);

        var tasks = await _unitOfWork.TaskRepository
            .GetAll()
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.Deadline < DateTime.Today && x.Status == false && x.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<GetTaskResponse>>(tasks);
        
        return new PaginationResponse<GetTaskResponse>
        {
            Items = result,
            CurrentPage = request.Page,
            Pages = (int)pageCount
        };
    }
}