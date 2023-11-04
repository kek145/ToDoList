using System.Threading.Tasks;
using ToDoList.DAL.Repositories.TaskRepository;

namespace ToDoList.DAL.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    ITaskRepository TaskRepository { get; }
    Task<bool> CommitAsync();
}