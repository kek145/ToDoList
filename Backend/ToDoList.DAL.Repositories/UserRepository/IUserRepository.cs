using ToDoList.Domain.Contracts.Response;

namespace ToDoList.DAL.Repositories.UserRepository;

public interface IUserRepository : IGenericRepository<UserEntity>
{
    Task<GetUserFullNameResponse> GetUserFullNameAsync(int userId);
}