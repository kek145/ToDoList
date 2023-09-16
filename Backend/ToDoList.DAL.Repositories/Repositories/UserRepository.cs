using ToDoList.DAL.Contracts.Interfaces;
using ToDoList.DAL.Core.DataContext;
using ToDoList.Domain.Entities.DbSet;

namespace ToDoList.DAL.Repositories.Repositories;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) {}
}