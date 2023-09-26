using ToDoList.DAL.Repositories.TaskRepository;
using ToDoList.DAL.Repositories.TokenRepository;

namespace ToDoList.DAL.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    ITokenRepository TokenRepository { get; }
    ITaskRepository TaskRepository { get; }

    Task<bool> CommitAsync();
}