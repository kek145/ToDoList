using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.DAL.Repositories.UserRepository;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) {}
    public async Task<GetUserFullNameResponse> GetUserFullNameAsync(int userId)
    {
        var user = await Context.Users.FindAsync(userId);

        if (user == null)
            return null!;

        var fullName = $"{user.FirstName} {user.LastName}";

        return new GetUserFullNameResponse
        {
            FullName = fullName
        };
    }
}