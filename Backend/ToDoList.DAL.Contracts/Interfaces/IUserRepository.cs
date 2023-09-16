using ToDoList.Domain.Entities.DbSet;

namespace ToDoList.DAL.Contracts.Interfaces;

public interface IUserRepository : IGenericRepository<UserEntity> { }