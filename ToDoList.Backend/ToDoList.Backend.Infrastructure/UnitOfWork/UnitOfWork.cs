using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;
using ToDoList.Infrastructure.DataStore;

namespace ToDoList.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public INoteRepository Notes { get; set; }
    
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, INoteRepository notes)
    {
        _context = context;
        Notes = notes;
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