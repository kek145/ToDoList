using System.Threading.Tasks;
using ToDoList.DAL.Configurations.DataContext;
using ToDoList.DAL.Repositories.TaskRepository;

namespace ToDoList.DAL.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public ITaskRepository TaskRepository { get; }

    public UnitOfWork(ApplicationDbContext context, ITaskRepository taskRepository)
    {
        _context = context;
        TaskRepository = taskRepository;
    }

    public async Task<bool> CommitAsync()
    {
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}