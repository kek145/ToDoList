namespace ToDoList.BL.Mediator.Queries.UserQueries;

public class GetUserFullNameHandler : IRequestHandler<GetUserFullNameQuery, GetUserFullNameResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetUserFullNameHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUserFullNameResponse> Handle(GetUserFullNameQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository
            .GetAll()
            .Where(x => x.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        var result = _mapper.Map<GetUserFullNameResponse>(user);

        return result;
    }
}