using System.Threading.Tasks;
using ToDoList.DAL.Core.DataContext;
using ToDoList.DAL.Contracts.Interfaces;

namespace ToDoList.DAL.Repositories.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public ITaskRepository TaskRepository { get; }
    public IUserRepository UserRepository { get; }

    public UnitOfWork(ApplicationDbContext context, ITaskRepository taskRepository, IUserRepository userRepository)
    {
        _context = context;
        TaskRepository = taskRepository;
        UserRepository = userRepository;
    }

    public async Task<bool> CommitAsync()
    {
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}