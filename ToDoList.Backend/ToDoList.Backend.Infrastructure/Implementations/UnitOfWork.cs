using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;
using ToDoList.Domain.Repositories;
using ToDoList.Infrastructure.DataStore;

namespace ToDoList.Infrastructure.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public IUserRepository Users { get; set; }
    public INoteRepository Notes { get; set; }
    public IRefreshTokenRepository RefreshTokens { get; set; }


    public UnitOfWork(
        ApplicationDbContext context,
        IUserRepository users,
        INoteRepository notes,
        IRefreshTokenRepository refreshTokens)
    {
        _context = context;
        Users = users;
        Notes = notes;
        RefreshTokens = refreshTokens;
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