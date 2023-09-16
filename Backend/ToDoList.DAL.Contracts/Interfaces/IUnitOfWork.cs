using System.Threading.Tasks;

namespace ToDoList.DAL.Contracts.Interfaces;

public interface IUnitOfWork
{
    ITaskRepository TaskRepository { get; }
    IUserRepository UserRepository { get; }
    Task<bool> CommitAsync();
}