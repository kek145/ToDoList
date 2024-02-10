using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;
using ToDoList.Domain.Repositories;
using ToDoList.Infrastructure.DataStore;

namespace ToDoList.Infrastructure.Implementations;

public class UnitOfWork(ApplicationDbContext context, IUserRepository users, INoteRepository notes, IRefreshTokenRepository refreshTokens) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;

    public IUserRepository Users { get; set; } = users;
    public INoteRepository Notes { get; set; } = notes;
    public IRefreshTokenRepository RefreshTokens { get; set; } = refreshTokens;

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