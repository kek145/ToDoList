using ToDoList.Domain.Entities.DbSet;
using ToDoList.DAL.Repositories.GenericRepository;

namespace ToDoList.DAL.Repositories.UserRepository;

public interface IUserRepository : IGenericRepository<UserEntity> { }