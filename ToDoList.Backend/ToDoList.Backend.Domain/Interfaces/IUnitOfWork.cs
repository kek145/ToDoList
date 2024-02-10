using System;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Repositories;

namespace ToDoList.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; set; }
    INoteRepository Notes { get; set; }
    IRefreshTokenRepository RefreshTokens { get; set; }

    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
}