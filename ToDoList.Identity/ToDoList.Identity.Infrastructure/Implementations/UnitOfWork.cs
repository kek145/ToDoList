using System.Threading;
using System.Threading.Tasks;
using ToDoList.Identity.Domain.Interfaces;
using ToDoList.Identity.Infrastructure.DataStore;

namespace ToDoList.Identity.Infrastructure.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public IUserRepository Users { get; }

    public UnitOfWork(ApplicationDbContext context, IUserRepository users)
    {
        _context = context;
        Users = users;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}