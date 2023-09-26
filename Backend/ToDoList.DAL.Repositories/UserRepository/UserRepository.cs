namespace ToDoList.DAL.Repositories.UserRepository;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) {}
}