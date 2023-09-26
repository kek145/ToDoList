namespace ToDoList.BL.Mediator.Queries.UserQueries;

public class GetUserInfoHandler : IRequestHandler<GetUserInfoQuery, GetUserInfoResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetUserInfoHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUserInfoResponse> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository
            .GetAll()
            .Where(x => x.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        var result = _mapper.Map<GetUserInfoResponse>(user);

        return result;
    }
}