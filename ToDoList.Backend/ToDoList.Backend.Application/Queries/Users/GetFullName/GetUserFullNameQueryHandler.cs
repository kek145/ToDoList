using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Response;

namespace ToDoList.Application.Queries.Users.GetFullName;

public class GetUserFullNameQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserFullNameQuery, UserFullNameResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task<UserFullNameResponse> Handle(GetUserFullNameQuery request, CancellationToken cancellationToken)
    {
        var fullName = await _userRepository.GetUserFullName(request.UserId, cancellationToken);
        
        var result = new UserFullNameResponse
        {
            FullName = fullName
        };
        return result;
    }
}